using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BatMovement : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField] private bool debugRays = true;
    private CharacterController controller;

    [Header("Settings")]
    [SerializeField] float flySpeed = 5.0f;
    [SerializeField] float flightHight = 2.0f;
    [SerializeField] LayerMask checkLayerForFlight;

    [Header("Tweeks")]
    [SerializeField] float steerSpeed = 100;
    [SerializeField] private float downForce = 10.0f;
    [SerializeField] float damping = 2.0f;

    private PlayerState playerState;

    //Movent Vector
    private Vector3 playerVelocity;

    //WARNING : For debug only 
    private bool RayHit;


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //TODO : Set back to Dracula
        playerState.SetState(PlayerState.playerStates.TransformToDracula);
    }

    private void Start()
    {
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
        if (Input.GetButtonDown("Fire1"))
            playerState.SetState(PlayerState.playerStates.TransformToDracula);
    }
    public void Init(PlayerState playerState)
    {
        this.playerState = playerState;
    }

    public void StartMove(Vector3 direction, PlayerState plState)
    {
        playerVelocity = direction;
        transform.forward = direction;
        this.playerState = plState;
    }

    public void MoveBat()
    {

        playerVelocity = BatControl();
        controller.Move(playerVelocity * Time.fixedDeltaTime);
        SetFaceForward();

        DebugRays();
    }

    private void SetFaceForward()
    {
        transform.forward = playerVelocity;
    }

    Vector3 BatControl()
    {
        Vector3 controllerDir = Quaternion.Euler(0, Input.GetAxis("Horizontal") * steerSpeed * Time.fixedDeltaTime, 0) * transform.forward;
        controllerDir = (controllerDir) * flySpeed;
        controllerDir.y = SphareCastGround();
        return controllerDir;
    }
    
    float SphareCastGround()
    {
        RaycastHit hit;

        RayHit = false;
        float yAxisSmoothAdjust = playerVelocity.y;

        if (Physics.SphereCast(transform.position + Vector3.up * controller.radius, controller.radius, -Vector3.up, out hit, flightHight, checkLayerForFlight))
        {
            //WARNING : Checking for standing still (Never happens)
            if (hit.point != transform.position - Vector3.up * flightHight)
            {
                Vector3 desiredPos = hit.point + Vector3.up * hit.normal.y * flightHight;
                float hightOff = 1/flightHight * (desiredPos.y - transform.position.y);

                hightOff *= hightOff;

                yAxisSmoothAdjust += hightOff + (Mathf.Abs(playerVelocity.y)) * Time.fixedDeltaTime;
                yAxisSmoothAdjust *= 1 - damping * Time.fixedDeltaTime;

                //WARNING : Only for debug
                RayHit = true;
            }
        }
        else
        {
            yAxisSmoothAdjust -= downForce * Time.fixedDeltaTime;
        }

        return yAxisSmoothAdjust;
    }
    
    void DebugRays()
    {
        if (debugRays)
        {
            if (RayHit)
            {
                if (debugRays)
                {
                    Debug.DrawRay(transform.position, playerVelocity * Time.fixedDeltaTime, Color.green, 10);
                }
            }
            else
            {
                if (debugRays)
                {
                    Debug.DrawRay(transform.position, playerVelocity * Time.fixedDeltaTime, Color.magenta, 10);
                }
            }
        }

    }
}