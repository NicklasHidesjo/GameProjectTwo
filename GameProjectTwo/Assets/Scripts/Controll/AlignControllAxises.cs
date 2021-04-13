using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignControllAxises : MonoBehaviour
{

    public bool debugRays = true;
    public Transform alignTo;
    public Vector3 alienedX;
    public Vector3 alienedZ;

    private Vector3 temp;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Align();
    }

    private void Init()
    {
        if (!alignTo)
        {
            alignTo = Camera.main.transform;
            Debug.Log("<color=red>No alingment transform. Auto Assigned : </color>" + alignTo.name);
        }

    }

    public void Align()
    {
        temp = alignTo.right;
        temp.y = 0;
        alienedX = temp.normalized;

        temp = alignTo.forward;
        temp.y = 0;
        alienedZ = temp.normalized;

        if (debugRays)
        {
            Debug.DrawRay(transform.position, alienedX * 5, Color.red);
            Debug.DrawRay(transform.position, alienedZ * 5, Color.blue);
        }
    }
}
