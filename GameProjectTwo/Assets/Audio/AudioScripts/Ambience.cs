using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    private AudioSource nightAmbienceSound;
    private SunTimeOfDay sunTimer;

    void Start()
    {
        nightAmbienceSound = GetComponent<AudioSource>();
        sunTimer = GetComponentInParent<SunTimeOfDay>();
        AudioManager.instance.PlaySound(SoundType.NightAmbience, gameObject, false);
    }

    private void Update()
    {
        if (sunTimer.TimeOfDay > 0)
        {
            nightAmbienceSound.volume = 1f - sunTimer.TimeOfDay / 7f;
        }
        
    }


}
