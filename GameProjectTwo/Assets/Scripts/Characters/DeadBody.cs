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


    private SpringJoint joint;
    [SerializeField] SpringJoint jointDefault;

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
            GetComponent<Rigidbody>().isKinematic = false;

            //RemoveJoint here
            RemoveJoint();

            gameObject.layer = startingLayer;

           // transform.parent = null;
            isGrabbed = false;
        }
        else
        {
          //  GetComponent<Rigidbody>().isKinematic = true;

            //Add joint here
            AddJoint(player, gameObject);
            
            
          //  transform.SetParent(player.transform, true);
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


    private void AddJoint(GameObject player, GameObject dragObject)
    {
        joint = player.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = player.transform.forward * 0.5f + player.transform.up;
        joint.anchor    = dragObject.transform.up;
        joint.connectedBody = dragObject.GetComponent<Rigidbody>();
    }
    
    private void RemoveJoint()
    {
        Destroy(joint);
    }
}
