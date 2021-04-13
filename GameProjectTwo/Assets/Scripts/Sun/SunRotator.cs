using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20f;

    void Update()
    {
        float newXRot = rotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(newXRot, 0, 0));
    }
}
