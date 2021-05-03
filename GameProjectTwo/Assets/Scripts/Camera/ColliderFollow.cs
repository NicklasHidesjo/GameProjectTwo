using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float wantedDist = 3;
    [SerializeField] Vector3 offsett = new Vector3(1, 1, -5);

    float ang;

    CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 targetP = target.position + Vector3.up;
        Vector3 dir = targetP - transform.position;
        float dist = dir.sqrMagnitude;
        float subdist = dist - wantedDist * wantedDist;
        dir = dir.normalized;
        dir *= subdist;

        //Rotate by mouse
        Vector3 newPos = transform.position + dir * Time.fixedDeltaTime;

        Vector3 rotatedPos = Quaternion.Euler(0, Input.GetAxis("Mouse X") * 6, 0) * (newPos - targetP);
        rotatedPos += targetP;
        rotatedPos = newPos - rotatedPos;
        dir = dir * Time.deltaTime + rotatedPos;

        //Sphare Overlap

        cc.Move(dir);
        transform.LookAt(target.position);

    }


}
