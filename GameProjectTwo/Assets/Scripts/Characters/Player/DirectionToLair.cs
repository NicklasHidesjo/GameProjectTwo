using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionToLair : MonoBehaviour
{
    SpriteRenderer lairFinder;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        lairFinder = gameObject.GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Lair").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(targetPosition);
        transform.Rotate(90, 0, 0);
        }
    }

}
