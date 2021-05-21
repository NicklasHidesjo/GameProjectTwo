using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLightAtDay : MonoBehaviour
{
    public void EnableLight(bool to)
    {
        GetComponent<Light>().enabled = to;

    }
}
