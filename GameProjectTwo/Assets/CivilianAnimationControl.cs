using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianAnimationControl : MonoBehaviour
{
    Animator anim;
    NPC npc;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        npc = GetComponent<NPC>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (npc.IsDead == false && anim.enabled== false)
        {
            anim.enabled = true;
        }

        if (npc.IsDead == true)
        {
           anim.SetInteger("VillagerState", 100);
           anim.enabled = false;
        }
        else
        {
            UpdateAnimator((int)npc.CurrentState);
            //Debug.Log($"This villager is {npc.CurrentState}, this equals {(int)npc.CurrentState}");
        }
    }

    public void UpdateAnimator(int i)
    {
        // Debug.Log($"incoming value of i={i}");
        if (anim.GetInteger("VillagerState") != i)
        {
            anim.SetInteger("VillagerState", i);
        }

        //Adjust the animationspeed on the fly
        //6 is walking atm.
        if (i ==6 || i== 9 || i == 0)
        {
            anim.speed = 3;
        }
        else
        {
            anim.speed = 1;
        }

    }
}
