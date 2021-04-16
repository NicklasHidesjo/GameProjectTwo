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
    float flightHight = 2;
    [SerializeField] LayerMask checkLayerForFlight;

    [SerializeField] float steerSpeed = 100;
    [SerializeField] private float downForce = 4f;

    private Vector3 dir;

    //Movent Vector
    private Vector3 playerVelocity;
    private Vector3 inputFormplayer;

    private Vector3 lastHitNormal;
    private Vector3 lastHitPoint;
    private Vector3 lastDir;
    private bool grounded;

    private Transform spawnPoint;

    float upForce;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

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
        lastDir = direction;
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

        SphareCastGround();
        inputFormplayer = dir * flySpeed;
//        inputFormplayer = Quaternion.FromToRotation(dir, lastDir) * inputFormplayer;
        inputFormplayer = (dir + lastDir) * flySpeed * 0.5f;


        if (grounded)
        {
            if (debugRays)
            {
                Debug.DrawRay(transform.position, inputFormplayer * Time.deltaTime, Color.green, 10);
            }
        }
        else
        {
            lastDir.y -= downForce * Time.deltaTime;

            if (debugRays)
            {
                Debug.DrawRay(transform.position, inputFormplayer * Time.deltaTime, Color.magenta, 10);
            }
        }
    }

    void SphareCastGround()
    {
        RaycastHit hit;

        grounded = false;
        if (Physics.SphereCast(transform.position + Vector3.up * controller.radius, controller.radius, -transform.up, out hit, controller.radius + flightHight, checkLayerForFlight))
        {
            if (hit.point != lastHitPoint)
            {
                Vector3 point = hit.point + Vector3.up * hit.normal.y * flightHight;
                Debug.DrawLine(hit.point, point, Color.cyan, 10);
                lastDir = Vector3.MoveTowards(lastDir, (point - lastHitPoint).normalized, 2f * Time.deltaTime);
                lastHitPoint = transform.position;
                grounded = true;
            }
        }
    }
}