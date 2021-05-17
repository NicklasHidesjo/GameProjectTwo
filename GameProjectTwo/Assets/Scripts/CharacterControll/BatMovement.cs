using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BatMovement : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField] private bool debugRays = true;
    [SerializeField] private float turnSpeed = 2;

    private float banking = 0;

    private CharacterController controller;

    [Header("Settings")]
    [SerializeField] float flySpeed = 5.0f;
    [SerializeField] float flightHight = 2.0f;
    [SerializeField] float maxFlightHight = 4.0f;
    [SerializeField] float minFlightHight = 2.0f;
    [SerializeField] LayerMask checkLayerForFlight;

    [Header("Tweeks")]
    [SerializeField] float batCostPerSec = 20;
    [SerializeField] float steerSpeed = 100;
    [SerializeField] private float downForce = 10.0f;
    [SerializeField] float damping = 2.0f;

    [Header("GRFX")]
    [SerializeField] float bankAmount = 30;

    private PlayerState playerState;

    //Movent Vector
    private Vector3 playerVelocity;
    private Vector3 inputFormplayerLast = Vector3.zero;

    //WARNING : For debug only 
    private bool RayHit;


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //TODO : Set back to Dracula
      //  playerState.SetState(PlayerState.playerStates.TransformToDracula);
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

        PlayerManager.instance.StatsManager.DecreaseStaminaValue(batCostPerSec * Time.deltaTime);

        if (Input.GetButtonDown("TransformShape") || PlayerManager.instance.StatsManager.CurrentStamina <= 0)
        {
            playerState.SetState(PlayerState.playerStates.TransformToDracula);
        }
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

    public void Move()
    {
        playerVelocity = BatControl();

        controller.Move(playerVelocity * Time.fixedDeltaTime);
        SetFaceForward();

        DebugRays();
    }

    private void SetFaceForward()
    {
        Vector3 faceDir = inputFormplayerLast;
        Vector3 flatDir = inputFormplayerLast;

        flatDir.y = 0;

        float sqrMag = flatDir.sqrMagnitude;
        if (sqrMag < 1)
        {
            faceDir.y += 1 - (Mathf.Sqrt(sqrMag));
        }
        
        transform.rotation = Quaternion.Euler(0, 0, banking * 0.75f) * Quaternion.LookRotation(faceDir.normalized);
    }

    Vector3 BatControl()
    {
        Vector3 inputFormplayer =
           Input.GetAxis("Horizontal") * FlatAlignTo(PlayerManager.instance.GetPlayerCam().transform.right) +
           Input.GetAxis("Vertical") * FlatAlignTo(PlayerManager.instance.GetPlayerCam().transform.forward);


        if (Vector3.Dot(inputFormplayer, transform.right) < 0)
        {

            banking = Vector3.Angle(inputFormplayerLast, inputFormplayer);
        }
        else
        {
            banking = Vector3.Angle(inputFormplayerLast, inputFormplayer)*-1;
        }
        inputFormplayerLast = Vector3.MoveTowards(inputFormplayerLast, inputFormplayer, turnSpeed * Time.fixedDeltaTime);


        inputFormplayer *= flySpeed;

        if (Input.GetButton("Jump"))
            flightHight = maxFlightHight;
        else
            flightHight = minFlightHight;


        inputFormplayer.y = SphareCastGround();
        return inputFormplayer;
    }


    Vector3 OldBatControl()
    {
        Vector3 controllerDir = Quaternion.Euler(0, Input.GetAxis("Horizontal") * steerSpeed * Time.fixedDeltaTime, 0) * transform.forward;
        //controllerDir += Quaternion.Euler(-Input.GetAxis("Vertical") * steerSpeed * Time.fixedDeltaTime, 0, 0) * transform.forward;
        flightHight = Input.GetAxis("Vertical") * 4;

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
                float hightOff = 1 / flightHight * (desiredPos.y - transform.position.y);

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


    Vector3 FlatAlignTo(Vector3 v)
    {
        v.y = 0;
        return v.normalized;
    }


}