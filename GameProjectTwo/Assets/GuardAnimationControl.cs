using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAnimationControl : MonoBehaviour
{
    Animator anim;
    NPC npc;
    NavMeshAgent nma;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        npc = GetComponent<NPC>();
        nma = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateAnimator((int)npc.CurrentState);
        //Debug.Log($"This guard is {npc.CurrentState}, this equals {(int)npc.CurrentState}");
    }

    public void UpdateAnimator(int i)
    {
       /* if(i == 18)
        {
            Debug.Log($"the X velocity is {nma.velocity.magnitude}");
        }*/

        //if the guard becomes idle after chasing Dracula, we set the animator to walking.
        if ((i == 18) && (nma.velocity.magnitude > 0.5f))
        {
            i = 16;
           // Debug.Log("idle->walk activated");
        }

        //Debug.Log($"incoming value of i={i}");
        if (anim.GetInteger("GuardState") != i)
        {
            anim.SetInteger("GuardState", i);
        }

        if (i == 16)
        {
            anim.speed = 2;
        }
        else
        {
            anim.speed = 1;
        }

    }
}
