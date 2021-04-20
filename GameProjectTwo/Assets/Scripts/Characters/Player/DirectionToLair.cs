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
            transform.LookAt(target);
            transform.Rotate(90, 0, 0);
        }
    }

    public void Activate()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

}
