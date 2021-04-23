using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverShoulderCam : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] LayerMask checkLayers;
    [SerializeField] Transform target;
    [SerializeField] Vector3 offsett;
    [SerializeField] float speed = 30;
    [SerializeField] float mouseSpeed = 3;

    Transform dummy;

    Vector3 trueOffset;
    [SerializeField] float mX = 40;
    [SerializeField] float mY;

    //Debug options
    [SerializeField] bool lockToWindow;

    float smooth;

    // Start is called before the first frame update
    void Start()
    {
        if(lockToWindow)
        Cursor.lockState = CursorLockMode.Confined;
        dummy = new GameObject().transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mX -= Input.GetAxis("Mouse Y") * mouseSpeed;
        if (mX > 80)
        {
            mX = 80;
        }
        else if (mX < -60)
        {
            mX = 0;
        }

        //mX = Mathf.Clamp(mY, -90, 90);
        mY += Input.GetAxis("Mouse X") * mouseSpeed;
        dummy.rotation = Quaternion.Euler(mX, mY, 0);

        RaycastHit hit;
        Vector3 tDir = camera.rotation * (Vector2)offsett;
        if (Physics.Raycast(target.position, tDir, out hit, Mathf.Abs( offsett.x), checkLayers))
        {
            offsett.x *= -1;
        }




        dummy.position = target.position + dummy.rotation * offsett;
        Vector2 off = offsett;
        Vector3 lookAtTarget = target.position + dummy.rotation * off;

        dummy.LookAt(lookAtTarget);
        trueOffset = Vector3.MoveTowards(trueOffset, RayCam(target.position, dummy.position), smooth + speed * Time.deltaTime);

        camera.position = target.position + dummy.rotation * trueOffset;
        camera.rotation = dummy.rotation;
    }

    Vector3 RayCam(Vector3 from, Vector3 to)
    {
        //TODO : If radius is larger then target collider radius, cast clips. Compensate by moveing ray
        //Boxcast /plane /dist to midpoint = movespeed? 
        Ray ray = new Ray(from,  to - from);


        Debug.DrawRay(ray.origin, ray.direction * offsett.z * -1, Color.green);

        //DebugBox(from, (from + to * 0.5f), Vector3.one - Vector3.up * 0.5f, Quaternion.LookRotation(ray.direction), Color.yellow);

        RaycastHit hit;
        /*
        if(Physics.BoxCast(ray.origin, Vector3.one - Vector3.up * 0.25f, ray.direction, out hit, Quaternion.identity, offsett.z *-1, checkLayers))
        {
            Debug.DrawRay(hit.point, hit.normal, Color.red);

            Vector3 point = hit.point - ray.direction * hit.distance;
            Debug.DrawRay(point, Vector3.one*0.1f, Color.cyan);

            smooth  = 1 - (ray.origin - point).sqrMagnitude;

            Debug.DrawRay(ray.origin, ray.direction * offsett.z * -1, Color.red);

            return new Vector3(offsett.x, offsett.y, Mathf.Lerp(offsett.z, hit.distance, smooth));
        }
        /*/
       

        //WORKS!!!
        if (Physics.SphereCast(ray, 0.45f, out hit, offsett.z *-1, checkLayers))
        {
            Debug.DrawRay(ray.origin, ray.direction * offsett.z * -1, Color.red);
            return new Vector3(offsett.x, offsett.y, -hit.distance);
        }
        
        return offsett;
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
