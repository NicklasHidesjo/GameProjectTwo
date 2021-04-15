using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField] private bool debugRays = true;
    private CharacterController controller;

    [Header("Settings")]
    [SerializeField] float flySpeed = 10f;
    [SerializeField] float steerSpeed = 100;
    [SerializeField] private float downForce = 4f;

    private Vector3 dir;

    //Movent Vector
    private Vector3 playerVelocity;
    private Vector3 inputFormplayer;

    private Vector3 lastHitNormal;
    private bool grounded;

    private Transform spawnPoint;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        lastHitNormal = hit.normal;
        if (Vector3.Dot(lastHitNormal, Vector3.up) < 0.5)
        {
            //Revert to batman
            Debug.Log("NoBats");
        }
        
        grounded = true;
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
        dir = direction;
    }

    public void MoveBat()
    {
        BatControll();
        playerVelocity = inputFormplayer;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void BatControll()
    {
        dir = Quaternion.Euler(0, Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime, 0) * dir;

        inputFormplayer = dir * flySpeed;
        inputFormplayer = Quaternion.FromToRotation(transform.up, lastHitNormal) * inputFormplayer;

        if (grounded)
        {
            playerVelocity.y = 0;

            if (debugRays)
            {
                Debug.DrawRay(transform.position, inputFormplayer * Time.deltaTime, Color.green, 10);
            }
        }
        else
        {
            inputFormplayer.y = playerVelocity.y - downForce * Time.deltaTime;

            if (debugRays)
            {
                Debug.DrawRay(transform.position, inputFormplayer * Time.deltaTime, Color.magenta, 10);
            }
        }
        grounded = false;
    }
}