using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : Interactable
{
    [SerializeField] Material selectedMaterial;
    Material standardMaterial;

    bool isGrabbed;
    bool isHidden;

    private void Start()
    {
        standardMaterial = GetComponent<MeshRenderer>().material;
    }

    public override void Interact(GameObject player)
    {
        if (isGrabbed)
        {
            isGrabbed = false;
            GetComponent<Rigidbody>().isKinematic = false;

            transform.parent = null;
        }
        else
        {

            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(player.transform, true);
            isGrabbed = true;

        }
    }

    public override void SetSelected(bool isSelected)
    {
        if (isSelected)
        {
            GetComponent<MeshRenderer>().material = selectedMaterial;

        }
        else
        {
            GetComponent<MeshRenderer>().material = standardMaterial;

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
