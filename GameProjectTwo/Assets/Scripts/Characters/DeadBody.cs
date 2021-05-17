using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//used to make any object carriable by the player
public class DeadBody : Interactable
{

    private bool isGrabbed;
    private bool isHidden;
    public bool IsHidden => isHidden;

   
   
    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddRelativeTorque(transform.forward * -100, ForceMode.Impulse);

    }

    public override void Interact(GameObject player)
    {
        if (isGrabbed)
        {
            // transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = false;

            isGrabbed = false;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
            //  transform.SetParent(player.transform, true);



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
