using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInteractionTowardsCamera : MonoBehaviour
{
    float offset;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.name == "CharmInteractRenderer")
        {
            offset = -0.5f;
        }
        else
        {
            offset = 0.5f;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = transform.parent.position+Vector3.down*offset;
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, 2);
        transform.LookAt(Camera.main.transform.position);
    }
}
