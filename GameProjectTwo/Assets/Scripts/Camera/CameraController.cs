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
    [SerializeField] LayerMask checkLayer;

    public enum cameraPriority { low, Medium, high }
    private cameraPriority currentPrio = cameraPriority.low;


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
            Debug.Log("<color=red> Target is missing. </color>");
        }
    }

    private void FixedUpdate()
    {
        if (target)
            CameraControll();
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
        boxSize.z = (pos - t).magnitude * 0.5f;

        if (debugRays)
        {
            DebugBox(t, t + rayDir * 0.5f + Vector3.up, boxSize, Quaternion.Euler(camEulerAngle), (Color.cyan + Color.clear)*0.5f);
        }


        RaycastHit hit;
        if (Physics.BoxCast(t + rayDir * 0.5f + Vector3.up * maxTowerHight, boxSize, Vector3.down, out hit, Quaternion.Euler(camEulerAngle), maxTowerHight - 1, checkLayer))
        {
            Vector3 ang = hit.point - t;

            camEulerAngle.x = 90 - Vector3.Angle(Vector3.up, ang.normalized);

            Vector3 offsettFromTarget = Quaternion.Euler(camEulerAngle.x, camEulerAngle.y, 0) * offsett + target.position;

            if (debugRays)
            {
                DebugBox(t, t + rayDir * 0.5f + Vector3.up, boxSize, Quaternion.Euler(camEulerAngle.x, camEulerAngle.y, 0), Color.red);
                Debug.DrawRay(hit.point, hit.normal, Color.red, 0.25f);
            }
            return offsettFromTarget;
        }
        return pos;
    }

    //Camera Targets

    //Set new camera target
    public void SetNewTarget(cameraPriority prio, Transform target)
    {
        this.target = target;
        currentPrio = prio;
    }

    public Transform OverrideSetTarget(cameraPriority prio, Transform target)
    {
        Transform oldTarget = this.target;
        if (prio > currentPrio)
        {
            this.target = target;
        }
        return oldTarget;
    }

    void DebugBox(Vector3 rotateAround, Vector3 center, Vector3 size, Quaternion rotation, Color col)
    {
        Vector3[] dirs = new Vector3[24];
        dirs[0] = new Vector3(size.x, size.y, size.z);
        dirs[1] = new Vector3(-size.x, size.y, size.z);

        dirs[2] = new Vector3(-size.x, size.y, size.z);
        dirs[3] = new Vector3(-size.x, -size.y, size.z);

        dirs[4] = new Vector3(-size.x, -size.y, size.z);
        dirs[5] = new Vector3(size.x, -size.y, size.z);

        dirs[6] = new Vector3(size.x, -size.y, size.z);
        dirs[7] = new Vector3(size.x, size.y, size.z);


        dirs[8] = new Vector3(size.x, size.y, size.z);
        dirs[9] = new Vector3(size.x, size.y, -size.z);

        dirs[10] = new Vector3(-size.x, size.y, size.z);
        dirs[11] = new Vector3(-size.x, size.y, -size.z);

        dirs[12] = new Vector3(-size.x, -size.y, size.z);
        dirs[13] = new Vector3(-size.x, -size.y, -size.z);

        dirs[14] = new Vector3(size.x, -size.y, size.z);
        dirs[15] = new Vector3(size.x, -size.y, -size.z);


        dirs[16] = new Vector3(size.x, size.y, -size.z);
        dirs[17] = new Vector3(-size.x, size.y, -size.z);

        dirs[18] = new Vector3(-size.x, size.y, -size.z);
        dirs[19] = new Vector3(-size.x, -size.y, -size.z);

        dirs[20] = new Vector3(-size.x, -size.y, -size.z);
        dirs[21] = new Vector3(size.x, -size.y, -size.z);

        dirs[22] = new Vector3(size.x, -size.y, -size.z);
        dirs[23] = new Vector3(size.x, size.y, -size.z);


        Vector3 offdir = (rotateAround - center);
        for (int i = 0; i < dirs.Length; i++)
        {
            dirs[i] -= Vector3.forward * boxSize.z;
            dirs[i] = rotation * dirs[i];
            dirs[i] += center + offdir;
        }

        for (int y = 0; y < dirs.Length; y += 2)
            Debug.DrawLine(dirs[y], dirs[y + 1], col);
    }


}
