using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimSound : MonoBehaviour
{
    private NPC npc;

    private void Start()
    {
        npc = GetComponentInParent<NPC>();
    }

    public void PlayAttackSound()
    {
        AudioManager.instance.PlayOneShot(SoundType.GuardAttack, gameObject);
    }

    public void HandleAttack()
    {      
        npc.Attack();
    }
}
