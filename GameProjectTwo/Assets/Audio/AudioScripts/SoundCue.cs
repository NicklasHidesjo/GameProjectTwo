using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundCue
{
    public SoundType type;
    public bool loop;
    public List<AudioClip> sounds;

}
