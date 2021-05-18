using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimationControl : MonoBehaviour
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
        UpdateAnimator((int)npc.CurrentState);
        //Debug.Log($"This guard is {npc.CurrentState}, this equals {(int)npc.CurrentState}");
    }

    public void UpdateAnimator(int i)
    {
       // Debug.Log($"incoming value of i={i}");
        if (anim.GetInteger("GuardState") != i)
        {
            anim.SetInteger("GuardState", i);
        }
    }
}
