using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimSound : MonoBehaviour
{
    public void PlayAttackSound()
    {
        AudioManager.instance.PlayOneShot(SoundType.GuardAttack, gameObject);
    }
}
