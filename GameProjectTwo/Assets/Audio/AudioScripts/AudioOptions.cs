using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOptions : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider masterSlider;

    private float musicVolume;
    private float soundVolume;
    private float masterVolume;

    public void SetSliders()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume", 0.75f);
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 0.75f);

    }

    public void SetMusicVolume()
    {
        musicVolume = musicSlider.value;
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
    }
    public void SetSoundVolume()
    {
        soundVolume = soundSlider.value;
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(soundVolume) * 20);
    }

    public void SetMasterVolume()
    {
        masterVolume = masterSlider.value;
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
    }

    private void OnDisable()
    {
        if (AudioManager.instance != null)
        {
            //AudioManager.instance.SaveVolumeSettings(musicVolume, soundVolume, masterVolume);
        }
    }
}
