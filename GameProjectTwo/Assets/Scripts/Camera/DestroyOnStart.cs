using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit)){
            transform.position = hit.point + Vector3.up;
        }
        Renderer r = gameObject.GetComponent<Renderer>();
       // Destroy(r);
    }
}
