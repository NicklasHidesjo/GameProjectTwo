using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverShoulderCam : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField] bool debugRays;

    [Header("Actors")]
    [SerializeField] Transform cam;
    [SerializeField] Transform target;
    Vector3 wantedPos = Vector3.zero;
    Quaternion wantedRotation = Quaternion.identity;


    [Header("Input Settings")]
    [SerializeField] float mouseSpeed = 6;
    [SerializeField] float maxLean = 2;
    private Vector3 lean;
    [SerializeField] float maxTilt = 80;
    [SerializeField] float minTilt = 15;

    //Target
    [Header("RayTrace Settings")]
    [Tooltip("Physicslayers Hit")]
    [SerializeField] LayerMask checkLayers;

    [Header("Camera Target Settings")]
    [Tooltip("Set Radius, Hight to cylinder cast around target")]
    [SerializeField] Vector2 t_CylinderSize = new Vector2(2, 1);
    [SerializeField] Vector3 targetOffset = new Vector3(0, 0.8f, 0);
    private Vector3 t_BoxSize;
    Vector3 targetPoint = Vector3.zero;
    private Collider[] t_CylinderHits = new Collider[8];

    //Camera
    [Header("Camera Position settings")]
    [Tooltip("Set the desired position of camera")]
    [SerializeField] Vector3 maxCameraOffsett = new Vector3(2, 1.5f, -10);
    [SerializeField] float cameraFromWallOffsett = 0.25f;
    [SerializeField] float rayRadius = 1;

    private RaycastHit[] hits = new RaycastHit[16];


    // Start is called before the first frame update
    void Start()
    {
        t_BoxSize = new Vector3(t_CylinderSize.x, t_CylinderSize.y * 0.5f, t_CylinderSize.x);
    }

    // Update is called once per frame
    void Update()
    {
        TargetLean();
        wantedRotation = MouseRotation(wantedRotation.eulerAngles);

        targetPoint = OffSetTarget(target.position + wantedRotation * lean+ targetOffset);

        wantedPos = GetWantedPosInRelationToTarget(targetPoint);

        cam.transform.position = (RayCam(targetPoint, wantedPos));
        cam.LookAt(targetPoint);
    }

    void TargetLean()
    {
        lean.x = Mathf.MoveTowards(lean.x, Input.GetAxis("Lean") * maxLean, 3*Time.deltaTime);

    }

    Vector3 OffSetTarget(Vector3 checkPos)
    {
        //This boxcast and then check whats in set radius around player, such as if inside cylinder
        //Returns a offsett adjusted point from walls inside cylinder

        if (debugRays)
        {
            DebugPoint(checkPos, t_BoxSize.x, Color.magenta);
        }

        Vector3 fusedNorm = Vector3.zero;
        int hitNum = Physics.OverlapBoxNonAlloc(checkPos, t_BoxSize, t_CylinderHits, Quaternion.identity, checkLayers);
        if (hitNum > 0)
        {
            for (int i = 0; i < hitNum; i++)
            {
                Vector3 point = t_CylinderHits[i].ClosestPoint(checkPos);
                Vector3 hitNorm = checkPos - point;
                float sqrDist = hitNorm.sqrMagnitude;

                //If inside Cylinder-ich
                if (sqrDist < t_CylinderSize.x * t_CylinderSize.x)
                {
                    float inf = (t_CylinderSize.x - Mathf.Sqrt(sqrDist));
                    
                    if (hitNorm == Vector3.zero)//<-- Inside collider
                    {
                        inf = t_CylinderSize.x;
                        hitNorm =  target.position- point;
                        
                    }
                    
                    fusedNorm += hitNorm.normalized * inf;
                }
            }

            fusedNorm.y = 0;
            DebugPoint(checkPos + fusedNorm, 0.5f, Color.white);
            return checkPos + fusedNorm / hitNum;
        }

        return checkPos;
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

    Vector3 GetWantedPosInRelationToTarget(Vector3 t_Point)
    {
        return t_Point + wantedRotation * Vector3.forward * maxCameraOffsett.z;
    }


    Vector3 RayCam(Vector3 from, Vector3 to)
    {
        //TODO : If radius is larger then target collider radius, cast clips. Compensate by moveing ray
        Ray ray = new Ray(from, to - from);
        int hitNum = Physics.SphereCastNonAlloc(ray.origin, rayRadius, ray.direction, hits, maxCameraOffsett.z * -1, checkLayers, QueryTriggerInteraction.Ignore);
        Vector3 closestHitPoint = to;

        if (hitNum > 0)
        {
            float compDist = Mathf.Infinity;
            for (int i = 0; i < hitNum; i++)
            {
                Collider col = hits[i].collider;
                //WARNING : +rayRadius
                Vector3 pos = PlanePoint(ray, col.ClosestPoint(ray.origin + ray.direction * (hits[i].distance + rayRadius)));
                Vector3 planeDirToC = ray.origin - pos;

                float lerp = planeDirToC.magnitude;
                lerp = lerp / rayRadius;
                lerp *= 2;
                lerp -= 1;

                Vector3 centerPoint = ray.origin + ray.direction * ((ray.origin - hits[i].point + planeDirToC).magnitude - 0.25f);
                pos = Vector3.Lerp(centerPoint, ray.origin + ray.direction * -maxCameraOffsett.z, lerp);


                float tDist = (pos - ray.origin).sqrMagnitude;
                if (tDist < compDist)
                {
                    closestHitPoint = pos;
                    compDist = tDist;
                }
            }
        }

        if (debugRays)
        {
            DebugPoint(from, 0.25f, Color.green);
            DebugPoint(to, 0.25f, Color.red);
            DebugBox(from, (from + to * 0.5f), new Vector3(1.0f, 1.0f, -maxCameraOffsett.z * 0.5f), Quaternion.LookRotation(ray.direction), (Color.yellow + Color.clear) * 0.5f);
            if (hitNum > 0)
            {
                DebugBox(from, (from + to * 0.5f), new Vector3(0.5f, 0.5f, -maxCameraOffsett.z * 0.5f), Quaternion.LookRotation(ray.direction), Color.red);
            }
            DebugPoint(closestHitPoint, 0.25f, Color.cyan);
        }

        return closestHitPoint;
    }

    Vector3 PlanePoint(Ray ray, Vector3 point)
    {
        Plane plane = new Plane(ray.direction, ray.origin);
        return plane.ClosestPointOnPlane(point);
    }


    void DebugPoint(Vector3 point, float scale, Color col)
    {
        Debug.DrawRay(point, Vector3.forward * scale, col);
        Debug.DrawRay(point, -Vector3.forward * scale, col);
        Debug.DrawRay(point, Vector3.right * scale, col);
        Debug.DrawRay(point, -Vector3.right * scale, col);
        Debug.DrawRay(point, Vector3.up * scale, col);
        Debug.DrawRay(point, -Vector3.up * scale, col);
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
