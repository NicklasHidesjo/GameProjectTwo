using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    Default,
    DraculaBite, DraculaDrink, DraculaDrinkDone, DraculaDamage, DraculaTransform, DraculaCoffin,
    GuardShout, GuardAttack, GuardSuspicious, GuardAlert, GuardSearching, GuardSearchingEnd,
    CivilianShout, CivilianDie, CivilianNotice,
    SunDamage, BatTransform, HideInContainer,
    MorningBell, NightAmbience,
}


public class AudioManager : MonoBehaviour
{

    private static AudioManager _instance;
    //[SerializeField] AudioList audioList; For use later if prefab gets too big
    public static AudioManager instance { get => _instance; }

    public List<SoundCue> soundCues;

    private GameObject audioListGameObject;
    [SerializeField] List<AudioSource> audioSources;
    [SerializeField] AudioMixer audioMixer;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSources = new List<AudioSource>();
        audioListGameObject = new GameObject("AudioSources");
        audioListGameObject.transform.parent = gameObject.transform;
        for (int i = 0; i < 10; i++)
        {
            AudioSource a = audioListGameObject.AddComponent<AudioSource>();         
            audioSources.Add(a);
        }

        float musicVolume = PlayerPrefs.GetFloat("musicVolume");
        float soundVolume = PlayerPrefs.GetFloat("soundVolume");
        float masterVolume = PlayerPrefs.GetFloat("masterVolume");

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            SaveVolumeSettings(0.75f, 0.75f, 0.75f);
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
            soundVolume = PlayerPrefs.GetFloat("soundVolume");
            masterVolume = PlayerPrefs.GetFloat("masterVolume");


        }

        SetMixerVolume(musicVolume, soundVolume, masterVolume);
    }

    private SoundCue GetCue(SoundType soundType)
    {
        SoundCue cue = soundCues.Find(x => x.type == soundType);
        if (cue == null)
        {
            Debug.LogWarning($"No Cue set for {soundType}, Using default value");
            return soundCues.Find(x => x.type == SoundType.Default);    
        }
        return cue;
    }

    private AudioSource GetIdleAudioSource()
    {
        if (audioSources.Exists(source => !source.isPlaying))
        {
            //Debug.Log("Found idle audioSource");
            return audioSources.Find(source => !source.isPlaying);
        }
        AudioSource s = audioListGameObject.AddComponent<AudioSource>();      
        audioSources.Add(s);
        Debug.LogWarning("No idle audioSource found, adding new one");

        return s;

    }

    public AudioSource PlaySound(SoundType soundType, float fadeInTime = 0f) //Play 2D sound
    {
        SoundCue cue = GetCue(soundType);

        AudioSource source = GetIdleAudioSource();
        source.clip = cue.sounds[Random.Range(0, cue.sounds.Count)];
        source.loop = cue.loop;
        source.outputAudioMixerGroup = cue.channel;
        if (fadeInTime == 0f)
        {
            source.Play();
        }
        else
        {
            StartCoroutine(AudioFadeIn(source, fadeInTime));
        }

        return source;
    }

    //Plays a sound, based on the enum, on the gameObject sent in.
    //Also returns the Audiosource if further modifications are needed. Made for 3D sounds
    public void PlaySound(SoundType soundType, GameObject obj) 
    {
        SoundCue cue = GetCue(soundType);
        AudioSource source = obj.GetComponent<AudioSource>();
        if (source == null)
        {
            Debug.LogWarning($"No audioSource Found on {obj}, Creating temp audiosource");
            source = obj.AddComponent<AudioSource>();
            source.spatialBlend = 1f;
            source.dopplerLevel = 0f;
            
        }
        
        source.clip = cue.sounds[Random.Range(0, cue.sounds.Count)];
        source.loop = cue.loop;
        source.Play();

    }

    //Plays a sound, based on the enum, on the gameObject sent in.
    //Oneshots are fire and forget, used to avoid interrupting whatever sound the Audio source is currently playing.
    //Made for 3D sounds
    public void PlayOneShot(SoundType soundType, GameObject obj) //Plays OneShot in 3D on the object
    {
        SoundCue cue = GetCue(soundType);
        AudioSource source = obj.GetComponent<AudioSource>();
        if (source == null)
        {
            Debug.LogWarning($"No audioSource Found on {obj}, Creating temp audiosource");
            source = obj.AddComponent<AudioSource>();
            source.spatialBlend = 1f;
            source.dopplerLevel = 0f;

        }

        source.outputAudioMixerGroup = cue.channel;
        source.PlayOneShot(cue.sounds[Random.Range(0, cue.sounds.Count)]);

    }

    //Stops the sound playing on the Gameobject sent in. Fade out optional
    public void StopSound(GameObject obj, float fadeOutTime = 0f)
    {
        AudioSource source = obj.GetComponent<AudioSource>();
        if (source == null)
        {
            Debug.LogWarning($"No audioSource Found on {obj}");
            return;
        }

        if (!source.isPlaying)
        {
            Debug.Log($"Audio on {obj} isn´t playing");
            return;
        }

        if (fadeOutTime == 0f)
        {
            source.Stop();
            return;
        }

        StartCoroutine(AudioFadeOut(source, fadeOutTime));

    }

    //Stops the Sound on the audiosource supplied. Fade out optional
    public void StopSound(AudioSource source, float fadeOutTime = 0f)
    {
        
        if (source == null)
        {
            Debug.LogWarning($"No audioSource Found on {source.gameObject}");
            return;
        }

        if (!source.isPlaying)
        {
            Debug.Log($"Audio on {source.gameObject} isn´t playing");
            return;
        }

        if (fadeOutTime == 0f)
        {
            source.Stop();
            return;
        }

        StartCoroutine(AudioFadeOut(source, fadeOutTime));

    }

    private IEnumerator AudioFadeOut(AudioSource source, float fadeOutTime)
    {
        print($"Fading out {source.clip}");
        
        float currentVolume = source.volume;
        for (float f = source.volume; f > 0; f -= Time.deltaTime/fadeOutTime)
        {
            source.volume = f;
            yield return null;
        }

        source.Stop();
        source.volume = currentVolume;
        print("finished fade");

    }

    private IEnumerator AudioFadeIn(AudioSource source, float fadeInTime)
    {
        //Maybe problematic when used together with fadeout on the same audiosource
        print($"Fading In {source.clip}");

        float currentVolume = source.volume;
        
        source.Play();
        for (float f = 0f; f > currentVolume; f += Time.deltaTime / fadeInTime)
        {
            source.volume = f;
            yield return null;
        }

        source.volume = currentVolume;
        print("finished fade");


    }

    public void StopAll2DSounds()
    {
        foreach (AudioSource a in audioSources)
        {
            if (a.isPlaying)
            {
                a.Stop();
            }
        }
    }

    public void SetMixerVolume(float musicVolume, float soundVolume, float masterVolume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(soundVolume) * 20);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
    }

    public void SaveVolumeSettings(float musicVolume, float soundVolume, float masterVolume)
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
        PlayerPrefs.SetFloat("masterVolume", masterVolume);

    }



}
