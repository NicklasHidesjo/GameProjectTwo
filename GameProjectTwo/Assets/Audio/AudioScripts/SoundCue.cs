using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundCue
{
    public SoundType type;
    public bool loop;
    public AudioMixerGroup channel;
    public List<AudioClip> sounds;

}
