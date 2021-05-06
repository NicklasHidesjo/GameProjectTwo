using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SoundList", menuName = "SoundList")]
public class AudioList : ScriptableObject
{
    public List<SoundCue> soundCues;
}


