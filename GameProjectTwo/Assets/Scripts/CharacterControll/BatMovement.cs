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


    //Movent Vector
    private Vector3 controllerDir;
    private Vector3 inputFromplayer;
    private Vector3 playerVelocity;

    private Vector3 smoothDir;
    private bool RayHit;


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //TODO : Set back to Dracula
    }

    private void Start()
    {
        Init();
    }

    private void Init()
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

    public void StartMove(Vector3 direction)
    {
        controllerDir = direction;
        smoothDir = direction;
    }

    public void MoveBat()
    {
        BatControl();
        playerVelocity = inputFromplayer;
        controller.Move(playerVelocity * Time.fixedDeltaTime);
    }

    void BatControl()
    {
        controllerDir = Quaternion.Euler(0, Input.GetAxis("Horizontal") * steerSpeed * Time.fixedDeltaTime, 0) * controllerDir;
        SphareCastGround();
        controllerDir.y = smoothDir.y;
        inputFromplayer = (controllerDir.normalized) * flySpeed;


        if (debugRays)
        {
            if (RayHit)
            {
                if (debugRays)
                {
                    Debug.DrawRay(transform.position, inputFromplayer * Time.fixedDeltaTime, Color.green, 10);
                }
            }
            else
            {
                if (debugRays)
                {
                    Debug.DrawRay(transform.position, inputFromplayer * Time.fixedDeltaTime, Color.magenta, 10);
                }
            }
        }
    }

    void SphareCastGround()
    {
        RaycastHit hit;

        RayHit = false;
        if (Physics.SphereCast(transform.position + Vector3.up * controller.radius, controller.radius, -transform.up, out hit, flightHight, checkLayerForFlight))
        {
            //WARNING : Check for standing still (Never happens)
            if (hit.point != transform.position - Vector3.up * flightHight)
            {
                Vector3 desiredPos = hit.point + Vector3.up * hit.normal.y * flightHight;
                float hightOff = desiredPos.y - transform.position.y;
                
                hightOff *= hightOff;

                smoothDir.y += (hightOff + Mathf.Abs(playerVelocity.y)) * Time.fixedDeltaTime;
                smoothDir.y += hightOff * Time.fixedDeltaTime;
                smoothDir.y *= 1 - damping* Time.fixedDeltaTime;

                //WARNING : Only for debug
                RayHit = true;
            }
        }
        else
        {
            smoothDir.y -= downForce * Time.fixedDeltaTime;
        }
    }
}