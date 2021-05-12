using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraculaAnimationControl : MonoBehaviour
{
    Animator anim;
    CharacterController cc;
    enum animStates { idle, walk, run, suck, pickUp };
    [SerializeField] animStates animState;

    [SerializeField] float walkthreshold = 0.25f;
    [SerializeField] float runthreshold = 4.9f;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        cc = GetComponent<CharacterController>();
    }

    private void LateUpdate()
    {
        //Set AnimationState by character movement
        animState = animStates.idle;
        float speed = cc.velocity.magnitude;

        if (speed > walkthreshold)
            animState = animStates.walk;

        if (speed >= runthreshold)
            animState = animStates.run;


        if (PlayerManager.instance.PlayerState.CurrentState == PlayerState.playerStates.DraculaSucking)
        {
            animState = animStates.suck;
        }
        if (PlayerManager.instance.PlayerState.CurrentState == PlayerState.playerStates.DraculaHideing ||
            PlayerManager.instance.PlayerState.CurrentState == PlayerState.playerStates.DraculaHidden ||
            PlayerManager.instance.PlayerState.CurrentState == PlayerState.playerStates.DraculaDragBody)
        {
            animState = animStates.pickUp;
        }


        //print((int)animState + " <<State>> " + anim.GetInteger("state") + " velocity : " + speed);
        UpdateAnimator((int)animState, speed);
    }

    // Update Animator int "state"
        // 0 idle
        // 1 walk
        // 2 run
        // 3 suck
        // 4 pickup
    public void UpdateAnimator(int i, float speed)
    {

        if (anim.GetInteger("state") != i)
            anim.SetInteger("state", i);

        //Set Matching animationspeed on walk and run
        if (i == 1)
        {
            anim.speed = speed;
        }
        else if (i == 2)
        {
            anim.speed = speed * 0.5f;
        }
        else
        {
            anim.speed = 1;
        }
    }
}
