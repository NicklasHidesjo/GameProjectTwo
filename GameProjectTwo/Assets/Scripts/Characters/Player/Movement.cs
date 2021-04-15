using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField] private bool debugRays = true;
    private CharacterController controller;
    
    [Header("Settings")]
    [SerializeField] private Transform cam;

    [SerializeField] float playerSpeed = 4.0f;
    [SerializeField] float jumpForce = 4.0f;
    [SerializeField] float normalGravity = 20f;
    [SerializeField] float holdJumpGravityUp = 6f;
    [SerializeField] float holdJumpGravityDown = 16f;
    
    //Alignment
    private Vector3 alienedX;
    private Vector3 alienedZ;

    //Movent Vector
    private Vector3 playerVelocity;
    private float applyedGravity;
    private Vector3 inputFormplayer;
    private Vector3 temp;

    private void Start()
    {
        Init();
    }
    void Update()
    {
        //MoveCharacter();
    }

    private void Init()
    {
        cam = Camera.main.transform;

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

    public void MoveCharacter()
    {
        applyedGravity = normalGravity;

        if (controller.isGrounded)
        {
            GroundControll();
            playerVelocity = inputFormplayer;
        }
        else
        {
            AirialControll();
        }
        AddGravity();
        ExecuteMove();
    }

    public void UpdateCharacter()
    {
        applyedGravity = normalGravity;
        AddGravity();
        ExecuteMove();
    }

    void GroundControll()
    {
        inputFormplayer = Input.GetAxis("Horizontal") * alienedX + Input.GetAxis("Vertical") * alienedZ;
        inputFormplayer *= playerSpeed;

        AlignControllerToCamera();

        if (playerVelocity.y < 0)
        {
            inputFormplayer.y = -applyedGravity * Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
        {
            inputFormplayer.y = jumpForce;
        }
    }

    void AlignControllerToCamera()
    {
        temp = cam.right;
        temp.y = 0;
        alienedX = temp.normalized;

        temp = cam.forward;
        temp.y = 0;
        alienedZ = temp.normalized;
    }

    void AirialControll()
    {
        if (Input.GetButton("Jump"))
        {
            if (playerVelocity.y < 0)
            {
                applyedGravity = holdJumpGravityUp;
            }
            else
            {
                applyedGravity = holdJumpGravityDown;
            }
        }
    }

    void AddGravity()
    {
        playerVelocity.y -= applyedGravity * Time.deltaTime;
    }

    void ExecuteMove()
    {
        controller.Move(playerVelocity * Time.deltaTime);

        if (debugRays)
        {
            Debug.DrawRay(transform.position, alienedX * playerVelocity.x * 5, Color.red);
            Debug.DrawRay(transform.position, alienedZ * playerVelocity.z * 5, Color.blue);
            Debug.DrawRay(transform.position, Vector3.up * playerVelocity.y * 5, Color.green);
        }
    }
}