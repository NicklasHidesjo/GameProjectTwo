using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierLayerMask : MonoBehaviour
{
   // [SerializeField] private LayerMask layerMask; --Did not work as intended.

    void Start()
    {
        //this works, should work fine in project settings to do this once aswell.
        Physics.IgnoreLayerCollision(7, 2);
    }


}
