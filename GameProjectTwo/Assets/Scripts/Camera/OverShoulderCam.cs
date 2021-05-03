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
    [SerializeField] float leanSpeed = 2;
    private Vector3 lean = Vector3.zero;
    [SerializeField] float maxTilt = 80;
    [SerializeField] float minTilt = 15;

    //Target
    [Header("RayTrace Settings")]
    [Tooltip("Physicslayers Hit")]
    [SerializeField] LayerMask checkLayers;

    [Header("Camera Target Settings")]
    [Tooltip("Set Radius, Hight to cylinder cast around target")]
    [SerializeField] float targetCylinderHight = 1;
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
    private float rayR;

    private RaycastHit[] hits = new RaycastHit[16];

    // Start is called before the first frame update
    void Start()
    {
        t_BoxSize = new Vector3(rayRadius, targetCylinderHight * 0.5f, rayRadius);
        Init();
    }

    void Init()
    {
        if (!target)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        if (!cam)
            cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {

        lean.x = TargetLean();
        wantedRotation = MouseRotation(wantedRotation.eulerAngles);

        wantedPos = target.position + targetOffset + wantedRotation * (lean * maxLean);
        wantedPos = RayCastOffsettPosition(target.position + targetOffset, wantedPos);

        rayR = GetDistanceToClosestCollider(target.position + targetOffset, wantedPos);
        targetPoint = wantedPos;

        // DebugPoint(targetPoint, 0.25f, Color.white);
        // Debug.DrawRay(targetPoint, Vector3.forward * rayR, Color.blue);

        wantedPos = GetWantedCameraPosition(targetPoint);
        // CylinderCast(targetPoint, wantedPos);

        cam.transform.position = Vector3.MoveTowards(cam.position, (RayCastCameraPosition(targetPoint, wantedPos)), 100 * Time.deltaTime);
        cam.rotation = Quaternion.LookRotation(targetPoint - wantedPos);
        //cam.LookAt(targetPoint);


        DebugDirectionArrow(Vector3.zero, Vector3.one * 9, 0.25f, Color.blue);
        DebugLineArrow(Vector3.one, Vector3.one * 9, 0.25f, Color.magenta);
    }

    float TargetLean()
    {
        return Mathf.MoveTowards(lean.x, Input.GetAxis("Lean"), leanSpeed * Time.deltaTime);
    }

    Vector3 RayCastOffsettPosition(Vector3 from, Vector3 checkPos)
    {
        //TODO : if hit no need to check area!
        //Raycast in case we were inside collider
        //   Debug.DrawLine(from, checkPos, Color.red);
        RaycastHit hit;
        if (Physics.Linecast(from, checkPos, out hit, checkLayers, QueryTriggerInteraction.Ignore))
        {
            //    Debug.DrawLine(from, hit.point, Color.green);
            return hit.point + (from - checkPos).normalized * 0.35f;
        }
        return checkPos;
    }

    float GetDistanceToClosestCollider(Vector3 from, Vector3 checkPos)
    {

        //This boxcast and then check whats in set radius around player, such as if inside cylinder
        //Returns a offsett adjusted point from walls inside cylinder

        //TODO : Smallest distace is triangle "B" Hight
        if (debugRays)
        {
            DebugPoint(checkPos, t_BoxSize.x, Color.magenta);
        }

        float distToWall = rayRadius;
        int hitNum = Physics.OverlapBoxNonAlloc(checkPos, t_BoxSize, t_CylinderHits, Quaternion.identity, checkLayers);

        if (hitNum > 0)
        {
            distToWall *= rayRadius;

            for (int i = 0; i < hitNum; i++)
            {
                Vector3 closestPointOnCollider = GetClosestPointOnColliderByType(checkPos, t_CylinderHits[i]);

                float targetToPointDistance = (checkPos - closestPointOnCollider).sqrMagnitude;
                if (targetToPointDistance < distToWall)
                {
                    distToWall = targetToPointDistance;
                }
            }

            distToWall = Mathf.Sqrt(distToWall);
        }

        return distToWall;
    }

    Vector3 GetClosestPointOnColliderByType(Vector3 checkPoint, Collider col)
    {
        //If Collider is convex we cant get the colliders ClosestPoint()
        if (!col.GetComponent<MeshCollider>())
        {
            return col.ClosestPoint(checkPoint);
        }
        else
        {
            if (col.GetComponent<MeshCollider>().sharedMesh)
                //TODO : Raycast
                return col.ClosestPointOnBounds(checkPoint);
            else
                return col.ClosestPoint(checkPoint);
        }
    }
    /*
    Vector3 OffSetTarget(Vector3 checkPos)
    {
        //This boxcast and then check whats in set radius around player, such as if inside cylinder
        //Returns a offsett adjusted point from walls inside cylinder

        //TODO : Smallest distace is triangle "B" Hight
        if (debugRays)
        {
            DebugPoint(checkPos, t_BoxSize.x, Color.magenta);
        }

        rayR = rayRadius;

        Vector3 fusedNorm = Vector3.zero;
        int hitNum = Physics.OverlapBoxNonAlloc(checkPos, t_BoxSize, t_CylinderHits, Quaternion.identity, checkLayers);
        if (hitNum > 0)
        {
            for (int i = 0; i < hitNum; i++)
            {
                Vector3 point;
                if (t_CylinderHits[i].GetComponent<MeshCollider>())
                {
                    if (t_CylinderHits[i].GetComponent<MeshCollider>().sharedMesh)
                        point = t_CylinderHits[i].ClosestPointOnBounds(checkPos);
                    else
                        point = t_CylinderHits[i].ClosestPoint(checkPos);
                }
                else
                {
                    point = t_CylinderHits[i].ClosestPoint(checkPos);
                }


                Vector3 hitNorm = checkPos - point;
                float sqrDist = hitNorm.sqrMagnitude;

                //If inside Cylinder-ich
                if (sqrDist < t_CylinderSize.x * t_CylinderSize.x)
                {
                    float inf = (t_CylinderSize.x - Mathf.Sqrt(sqrDist));

                    if (hitNorm == Vector3.zero)//<-- Inside collider
                    {
                        inf = t_CylinderSize.x;
                        hitNorm = target.position - point;

                    }

                    fusedNorm += hitNorm.normalized * inf;
                }

                float distToPoint = (checkPos - point).sqrMagnitude;
                if (distToPoint < rayR * rayR)
                {
                    rayR = Mathf.Sqrt(distToPoint);
                }
            }

            rayR -= 0.1f;
            fusedNorm.y = 0;
            DebugPoint(checkPos + fusedNorm, 0.5f, Color.white);
            return checkPos + fusedNorm / hitNum;
        }

        return checkPos;
    }
    */
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

    Vector3 GetWantedCameraPosition(Vector3 t_Point)
    {
        return t_Point + wantedRotation * Vector3.forward * maxCameraOffsett.z;
    }


    Vector3 RayCastCameraPosition(Vector3 from, Vector3 to)
    {
        //TODO : If radius is larger then target collider radius, cast clips. Compensate by moveing ray
        Ray ray = new Ray(from, to - from);
        Vector3 closestHitPoint = to;
        Vector3 origin = from - ray.direction * rayR;
        int hitNum = Physics.SphereCastNonAlloc(origin, rayR, ray.direction, hits, -maxCameraOffsett.z, checkLayers, QueryTriggerInteraction.Ignore);

        if (hitNum > 0)
        {
            float compDist = Mathf.Infinity;
            for (int i = 0; i < hitNum; i++)
            {
                if (IsValidHit(hits[i].point, hits[i].normal, ray.direction))
                {
                    //Calculate the intersection point between out ray and the hitpoint surface. 
                    float dot = Vector3.Dot(hits[i].normal, ray.direction);
                    float hypLength = (rayR - cameraFromWallOffsett) / Mathf.Abs(dot);
                    Vector3 farPoint = hits[i].point + hits[i].normal * rayR + ray.direction * hypLength;

                    Debug.DrawRay(hits[i].point, hits[i].normal, (Color.green + Color.clear) * 0.5f);
                    Debug.DrawRay(hits[i].point, hits[i].normal * rayR + ray.direction * hypLength, (Color.green + Color.clear) * 0.5f);

                    //If point is behind checked position, dont move
                    if (Vector3.Dot(farPoint - to, ray.direction) > 0)
                    {
                        farPoint = to;
                    }

                    float tDist = (farPoint - ray.origin).sqrMagnitude;
                    if (tDist < compDist)
                    {
                        closestHitPoint = farPoint;
                        compDist = tDist;
                    }
                }
            }
        }

        if (debugRays)
        {
            DebugBox(from, (from + to * 0.5f), new Vector3(rayRadius, rayRadius, -maxCameraOffsett.z * 0.5f), Quaternion.LookRotation(ray.direction), (Color.yellow + Color.clear) * 0.5f);
            DebugDirectionArrow(origin, ray.direction * -maxCameraOffsett.z, 0.5f, (Color.yellow + Color.clear) * 0.5f);
            if (hitNum > 0)
            {
                DebugBox(from, (from + to * 0.5f), new Vector3(rayR, rayR, -maxCameraOffsett.z * 0.5f), Quaternion.LookRotation(ray.direction), Color.red);
                DebugDirectionArrow(origin, ray.direction * -maxCameraOffsett.z, 0.5f, Color.red);
            }
            DebugPoint(closestHitPoint, 0.25f, Color.cyan);
        }

        return closestHitPoint;
    }

    bool IsValidHit(Vector3 p, Vector3 n, Vector3 dir)
    {
        //Pretty silly bug. Supposedly I can get a hit result, if raycasting out from a collider, but this returns point zero and normal opposite of ray
        if (p == Vector3.zero && n == dir * -1)
            return false;

        return true;
    }



    /// //////////////////
    void DebugPoint(Vector3 point, float scale, Color col)
    {
        Debug.DrawRay(point, Vector3.forward * scale, col);
        Debug.DrawRay(point, -Vector3.forward * scale, col);
        Debug.DrawRay(point, Vector3.right * scale, col);
        Debug.DrawRay(point, -Vector3.right * scale, col);
        Debug.DrawRay(point, Vector3.up * scale, col);
        Debug.DrawRay(point, -Vector3.up * scale, col);
    }

    void DebugLineArrow(Vector3 start, Vector3 end, float headSize, Color col)
    {
        Vector3 dir = end - start;
        dir = dir.normalized;
        Debug.DrawLine(start, end, col);
        Debug.DrawLine(end, end + (Quaternion.LookRotation(dir) * Quaternion.Euler(45, 135, 0) * Vector3.up) * headSize, col);
        Debug.DrawLine(end, end + (Quaternion.LookRotation(dir) * Quaternion.Euler(135, 135, 0) * Vector3.up) * headSize, col);
        Debug.DrawLine(end, end + (Quaternion.LookRotation(dir) * Quaternion.Euler(225, 45, 0) * Vector3.up) * headSize, col);
        Debug.DrawLine(end, end + (Quaternion.LookRotation(dir) * Quaternion.Euler(315, 45, 0) * Vector3.up) * headSize, col);
    }

    void DebugDirectionArrow(Vector3 start, Vector3 direction, float headSize, Color col)
    {
        Vector3 end = start + direction;
        Vector3 dir = direction.normalized;
        Debug.DrawLine(start, end, col);
        Debug.DrawLine(end, end + (Quaternion.LookRotation(dir) * Quaternion.Euler(45, 135, 0) * Vector3.up) * headSize, col);
        Debug.DrawLine(end, end + (Quaternion.LookRotation(dir) * Quaternion.Euler(135, 135, 0) * Vector3.up) * headSize, col);
        Debug.DrawLine(end, end + (Quaternion.LookRotation(dir) * Quaternion.Euler(225, 45, 0) * Vector3.up) * headSize, col);
        Debug.DrawLine(end, end + (Quaternion.LookRotation(dir) * Quaternion.Euler(315, 45, 0) * Vector3.up) * headSize, col);
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

    void DebugBox(Vector3 center, Vector3 size, Quaternion rotation, Color col)
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


        for (int i = 0; i < dirs.Length; i++)
        {
            dirs[i] = rotation * dirs[i];
            dirs[i] += center;
        }

        for (int y = 0; y < dirs.Length; y += 2)
            Debug.DrawLine(dirs[y], dirs[y + 1], col);
    }

    /* Crap
     * 
     * 
    Vector3 PlanePoint(Ray ray, Vector3 point)
    {
        Plane plane = new Plane(ray.direction, ray.origin);
        return plane.ClosestPointOnPlane(point);
    }

    Vector3 CylinderCastCam(Vector3 from, Vector3 to)
    {
        //TODO : If radius is larger then target collider radius, cast clips. Compensate by moveing ray
        Ray ray = new Ray(from, to - from);
        Vector3 midP = from - ray.direction * 0.25f;

        Vector3 boxSize = new Vector3(rayR, rayR, 0.25f);
        Quaternion orientation = Quaternion.LookRotation(ray.direction);

        DebudArrow(from - ray.direction, to + ray.direction, 0.25f, Color.white);
        DebugBox((to + from) * 0.5f, new Vector3(rayR, rayR, maxCameraOffsett.z * 0.5f), orientation, Color.white);
        DebugBox(midP, boxSize, orientation, Color.red);

        int hitNum = Physics.BoxCastNonAlloc(midP, boxSize, ray.direction, hits, orientation, -maxCameraOffsett.z, checkLayers, QueryTriggerInteraction.Ignore);

        Vector3 closestHitPoint = to;

        print(hitNum);
        if (hitNum > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                DebugPoint(hits[i].point, 0.25f, (Color.red + Color.clear) * 0.5f);

                //Inside Cylinder
                if ((from - PlanePoint(ray, hits[i].point)).sqrMagnitude < rayR * rayR)
                {
                    DebugPoint(hits[i].point, 0.25f, (Color.green));
                }

            }
        }


        return closestHitPoint;
    }



    void CylinderCast(Vector3 from, Vector3 to)
    {
        Ray ray = new Ray(from, to - from);
        DebudArrow(ray.origin, to, 0.25f, Color.green);

        Vector3 midP = (from + to) * 0.5f;
        Quaternion rotation = Quaternion.LookRotation(ray.direction);

        Collider[] overlaps = new Collider[16];

        Vector3 boxSize = new Vector3(rayRadius, rayRadius, maxCameraOffsett.z * 0.5f);

        int hitNum = Physics.OverlapBoxNonAlloc(midP, boxSize, overlaps, rotation, checkLayers, QueryTriggerInteraction.Ignore);

        DebugBox(midP, boxSize, rotation, Color.white);


        if (hitNum > 0)
        {
            for (int i = 0; i < hitNum; i++)
            {
                Vector3 point = GetClosestPointOnColliderByType(from, overlaps[i]);
                DebugPoint(point, 0.25f, Color.red);
            }
        }



    }
*/
}
