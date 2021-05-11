using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHightForExitPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position + Vector3.up *100, Vector3.down, out hit))
        {
            transform.position = hit.point + Vector3.up;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
