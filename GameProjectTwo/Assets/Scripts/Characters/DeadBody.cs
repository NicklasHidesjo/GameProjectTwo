using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//used to make any object carriable by the player
public class DeadBody : Interactable
{

    private bool isGrabbed;
    private bool isHidden;
    public bool IsHidden => isHidden;

    private int startingLayer;

    private void Start()
    {
        standardMaterial = GetComponent<MeshRenderer>().material;
        gameObject.GetComponent<Rigidbody>().AddRelativeTorque(transform.forward * -100, ForceMode.Impulse);
        startingLayer = gameObject.layer;
    }

    public override void Interact(GameObject player)
    {
        if (isGrabbed)
        {
            isGrabbed = false;
            GetComponent<Rigidbody>().isKinematic = false;
            gameObject.layer = startingLayer;

            transform.parent = null;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(player.transform, true);
            gameObject.layer = 0;
            isGrabbed = true;
        }

    }

    public void SetHidden(bool status)
    {
        isHidden = status;
        if (isHidden)
        {
            //GetComponent<Collider>().enabled = false;
        }
        else
        {
            //GetComponent<Collider>().enabled = true;

        }



    }
}
