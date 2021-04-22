using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraculaMovement : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField] private bool debugRays = true;
    private CharacterController controller;

    [Header("Settings")]
    [SerializeField] private Transform alignTo;

    [SerializeField] float playerSpeed = 4.0f;
    [SerializeField] float jumpForce = 4.0f;
    [SerializeField] float normalGravity = 20f;
    [SerializeField] float holdJumpGravityUp = 6f;
    [SerializeField] float holdJumpGravityDown = 16f;

    //Movent Vector
    private Vector3 playerVelocity;
    private bool jump;
    private PlayerState playerState;

    private void Start()
    {
        if(!alignTo)
            alignTo = Camera.main.transform;

        if (!controller)
        {
            CharacterController cc = gameObject.GetComponent<CharacterController>();
            if (cc)
            {
                controller = cc;
            }
            else
            {
                gameObject.AddComponent<CharacterController>();
                Debug.Log("<color=red>CharacterController not assigned to :" + transform.name + "  (AutoCreated)</color>");
            }
        }
    }

    //TODO : Decide on Input metod and keys.
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            //playerState.SetState(PlayerState.playerStates.TransformToBat);
        }
    }

    public void Init(PlayerState playerState, Transform cam)
    {
        this.playerState = playerState;
        this.alignTo = cam;
    }

    public void Move()
    {
        float applyedGravity = normalGravity;

        if (controller.isGrounded)
        {
            playerVelocity = GroundControl();
        }
        else
        {
            applyedGravity = AerialControl();
        }
        playerVelocity.y -= AddGravity(applyedGravity);
        controller.Move(playerVelocity * Time.fixedDeltaTime);

        ForwardFromMovement();

        DebugRays();
    }

    Vector3 FlatAlignTo(Vector3 v)
    {
        v.y = 0;
        return v.normalized;
    }

    Vector3 GroundControl()
    {
        Vector3 inputFormplayer = 
            Input.GetAxis("Horizontal") * FlatAlignTo(alignTo.right) + 
            Input.GetAxis("Vertical") * FlatAlignTo(alignTo.forward);
        
        inputFormplayer *= playerSpeed;

        if (jump)
        {
            inputFormplayer.y = jumpForce;
            jump = false;
        }
        return inputFormplayer;
    }

    float AerialControl()
    {
        if (Input.GetButton("Jump"))
        {
            if (playerVelocity.y < 0)
            {
                return holdJumpGravityUp;
            }
            else
            {
                return holdJumpGravityDown;
            }
        }
        else
        {
            return normalGravity;
        }
    }

    float AddGravity(float g)
    {
        return g * Time.fixedDeltaTime;
    }

    private void ForwardFromMovement()
    {
        Vector3 forwardFromMovement = playerVelocity;
        forwardFromMovement.y = 0;

        if (forwardFromMovement.sqrMagnitude > 0)
        {
            transform.forward = forwardFromMovement;
        }
    }

    //TODO : Remove when Done
    void DebugRays()
    {
        if (debugRays)
        {
            Debug.DrawRay(transform.position, playerVelocity * 5, Color.green);
        }
    }
}