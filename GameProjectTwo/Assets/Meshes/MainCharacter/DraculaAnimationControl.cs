using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraculaAnimationControl : MonoBehaviour
{
    Animator anim;
    CharacterController cc;
    Player player;
    bool isWalking;
    enum animStates { idle, walk, run, suck, pickUp, hiding, unhiding, };
    [SerializeField] animStates animState;

    [SerializeField] float walkthreshold = 0.25f;
    [SerializeField] float runthreshold = 4.9f;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("you pressed 2, wow");
            anim.SetBool("Charm", true);
        }
        else
        {
            anim.SetBool("Charm", false);
        }
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

        switch (player.CurrentState)
        {
            case PlayerStates.DraculaDragBody:
                animState = animStates.pickUp;
                break;
            case PlayerStates.DraculaHideing: 
                animState = animStates.hiding;
                break;
            case PlayerStates.DraculaStopHiding:
                animState = animStates.unhiding;
                break;
            case PlayerStates.DraculaHidden:                
                break;
            case PlayerStates.DraculaSucking:
                animState = animStates.suck;
                break;
            case PlayerStates.DraculaBurning:
                break;
            case PlayerStates.TransformToBat:
                break;
            case PlayerStates.BatDefault:
                break;
            default:
                break;
        }

        if(speed >0.25f)
        {
            isWalking=true;
        }
        else
        {
            isWalking = false;
        }

        //print((int)animState + " <<State>> " + anim.GetInteger("state") + " velocity : " + speed );
        UpdateAnimator((int)animState, speed, isWalking);
    }

    // Update Animator int "state"
        // 0 idle
        // 1 walk
        // 2 run
        // 3 suck
        // 4 pickup
    public void UpdateAnimator(int i, float speed, bool isWalking)
    {
        anim.SetBool("IsWalking",isWalking); 
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
