using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("DEBUG")]
    public bool debugRays;
    [SerializeField] private float maxTowerHight = 100;
    [SerializeField] private Vector3 boxSize = new Vector3(0.5f, 1, 1);

    [Header("Cam Controll")]
    [SerializeField] Camera cam;
    [SerializeField] Transform target;

    [SerializeField] float PreferedAngle = 30;
    [Range(0, 2)]
    [SerializeField] float camSpeedMultiplyer = 0.75f;
    [SerializeField] float camDistance = 10;
    [SerializeField] float rotSpeed = 10;

    private Vector3 camEulerAngle;
    private Vector3 offsett;
    private Vector3 trueTarget;
    private float camSpeed;
    private Vector3 rayDir;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    // Update is called once per frame
    void Update()
    {
        CameraControll();
    }

    public void Init()
    {
        offsett = new Vector3(0, 0, -camDistance);
        if (!cam)
        {
            cam = Camera.main;

            Debug.Log("<color=red> Camara is missing. Auto assigned : </color>" + cam.name);
        }

        if (!target)
        {
            target = FindObjectOfType<CharacterController>().transform;

            Debug.Log("<color=red> Target is missing. Auto assigned : </color>" + target.name);
        }
    }


    public void CameraControll()
    {
        //Input
        camEulerAngle.x = PreferedAngle;
        camEulerAngle.y += Input.GetAxis("Mouse X") * rotSpeed;

        //Set true target pos
        trueTarget = Quaternion.Euler(camEulerAngle) * offsett + target.position;
        trueTarget = RayCheckPosition(trueTarget, target.position);

        //Smooth target pos
        camSpeed = (cam.transform.position - trueTarget).sqrMagnitude * camSpeedMultiplyer;

        //Set Camera pos and look at target
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, trueTarget, camSpeed * Time.deltaTime);
        cam.transform.LookAt(target.position);
    }


    Vector3 RayCheckPosition(Vector3 pos, Vector3 t)
    {
        rayDir = pos - t;

        //TODO : Remove -2 and use layermask instead//////////////////////////////////////////////////
        boxSize.z = (pos - t).magnitude * 0.5f;
        boxSize.z -= 1;

        RaycastHit hit;
        if (Physics.BoxCast(t + rayDir * 0.5f + Vector3.up * maxTowerHight, boxSize, Vector3.down, out hit, Quaternion.Euler(camEulerAngle), maxTowerHight))
        {
            Vector3 ang = hit.point - t;
            camEulerAngle.x = 90 - Vector3.Angle(Vector3.up, ang.normalized);
            Vector3 offsettFromTarget = Quaternion.Euler(camEulerAngle.x, camEulerAngle.y, 0) * offsett + target.position;

            if (debugRays)
            {
                Debug.DrawRay(hit.point, hit.normal, Color.red, 0.25f);
                Debug.DrawRay(hit.point, ang, Color.blue, 0.25f);
            }
            return offsettFromTarget;
        }
        return pos;
    }
}
