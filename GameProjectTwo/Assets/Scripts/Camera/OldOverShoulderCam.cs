using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldOverShoulderCam : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField] bool debugRays;
    [Header("Settings")]
    [SerializeField] Transform cam;
    [SerializeField] LayerMask checkLayers;
    [SerializeField] Transform target;
    [SerializeField] Vector3 maxOffsett = new Vector3(2, 1.5f, -10);
    [SerializeField] float leanSpeed = 2.0f;
    [SerializeField] float speed = 60;
    [SerializeField] float mouseSpeed = 6;
    [SerializeField] float maxTilt = 80;
    [SerializeField] float minTilt = 10;

    //Debug options
    [SerializeField] bool lockToWindow;

    private float lean;
    Transform dummy;

    // Start is called before the first frame update
    void Start()
    {
        if (!cam)
            cam = Camera.main.transform;

        if (lockToWindow)
            Cursor.lockState = CursorLockMode.Confined;

        dummy = new GameObject().transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        dummy.rotation = MouseRotation(dummy.eulerAngles);

        Vector3 offsett = maxOffsett;
        offsett = Lean(offsett);

        //Rotate offsett
        Vector3 tOffsett = target.position + dummy.rotation * new Vector3(offsett.x, offsett.y, 0);
        offsett = target.position + dummy.rotation * offsett;
        offsett = RayCam(tOffsett, offsett);
        dummy.position = offsett;

        cam.position = Vector3.MoveTowards(cam.position, offsett, speed * Time.deltaTime);
        cam.LookAt(tOffsett);
    }

    Quaternion MouseRotation(Vector3 camEuler)
    {
        camEuler.y += Input.GetAxis("Mouse X") * mouseSpeed;
        camEuler.x -= Input.GetAxis("Mouse Y") * mouseSpeed;
        if (camEuler.x > maxTilt)
        {
            camEuler.x = maxTilt;
        }
        else if (camEuler.x < minTilt)
        {
            camEuler.x = minTilt;
        }

        return Quaternion.Euler(camEuler);
    }

    Vector3 Lean(Vector3 offsett)
    {
        lean *= 1 - leanSpeed * Time.deltaTime;
        lean += Input.GetAxis("Lean") * leanSpeed * 2 * Time.deltaTime;

        lean = Mathf.Clamp(lean, -maxOffsett.x, maxOffsett.x);


        offsett.x = lean;

        RaycastHit hit;
        Vector3 tDir = dummy.rotation * (Vector2)offsett;
        if (Physics.Raycast(target.position, tDir, out hit, lean, checkLayers))
        {
            if (lean > 0)
                lean = hit.distance;
            else
                lean = -hit.distance;
        }

        offsett.x = lean;
        return offsett;
    }

    Vector3 RayCam(Vector3 from, Vector3 to)
    {
        //TODO : If radius is larger then target collider radius, cast clips. Compensate by moveing ray

        Ray ray = new Ray(from, to - from);
        RaycastHit hit;

        if (debugRays)
            DebugBox(from, (from + to * 0.5f), new Vector3(0.5f, 0.5f, -maxOffsett.z * 0.5f), Quaternion.LookRotation(ray.direction), (Color.yellow + Color.clear) * 0.5f);

        if (Physics.SphereCast(ray, 0.45f, out hit, maxOffsett.z * -1, checkLayers))
        {
            Plane plane = new Plane(ray.direction, from);
            Vector3 p = plane.ClosestPointOnPlane(hit.point);
            Vector3 offDir = p - from;
            offDir += hit.normal * 0.5f * maxOffsett.y;
            offDir *= Vector3.Dot(ray.direction, hit.normal);

            Vector3 newPos = from + offDir + ray.direction * hit.distance;

            if (debugRays)
                DebugBox(from, (from + to * 0.5f), new Vector3(0.5f, 0.5f, -maxOffsett.z * 0.5f), Quaternion.LookRotation(ray.direction), Color.red);

            return newPos;
        }
        return to;
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
            dirs[i] -= Vector3.forward * -size.z;
            dirs[i] = rotation * dirs[i];
            dirs[i] += center + offdir;
        }

        for (int y = 0; y < dirs.Length; y += 2)
            Debug.DrawLine(dirs[y], dirs[y + 1], col);
    }
}
