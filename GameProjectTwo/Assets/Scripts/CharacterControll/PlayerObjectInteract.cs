using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Used to interact with objects found with interactable scanner
[RequireComponent(typeof(InteractableScanner))]
public class PlayerObjectInteract : MonoBehaviour
{
    InteractableScanner iScanner;
    Interactable interactedBarrel;
    Interactable heldInteractable;
    private Player player;
    

    void Start()
    {
        iScanner = GetComponent<InteractableScanner>();
        player = GetComponent<Player>();
    }

    public void InteractWithObject(Interactable interactable)
    {
        switch (interactable)
        {
            case DeadBody D: //checks on player to see if interaction is valid
                {
                    if (heldInteractable != null)
                    {
                        Debug.Log("Need to let go first: " + D.gameObject);
                        return;
                    }
                    Debug.Log("Interact deadbody: " + D.gameObject);
                    heldInteractable = D;
                    //transform.LookAt(D.transform);
                    interactable.Interact(gameObject);
                    iScanner.RemoveInteractableFromList(heldInteractable);
                    player.DraggingBody = true;
                    break;
                }
                
            case Container C: //checks on player to see if interaction is valid
                {
                    if (C.ObjectInside != null)
                    {

                        Debug.Log("Container is full");
                        return;

                    }
                    else if (heldInteractable != null)
                    {
                        heldInteractable.Interact(gameObject);
                        interactable.Interact(heldInteractable.gameObject);
                        heldInteractable = null;
                        player.DraggingBody = false;
                    }
                    else
                    {
                        Debug.Log("Entering " + C.gameObject);
                        GetComponent<CharacterController>().enabled = false;
                        interactable.Interact(gameObject);
                        player.Hiding = true;
                        interactedBarrel = interactable;
                    }
                    break;
                }
            case BloodSuckTarget B: //see if the object itself can validate interaction
                {
                    interactable.Interact(gameObject);
                    heldInteractable = B;
                    transform.LookAt(interactable.transform);
                    break;
                }

            default:
                break;
        }
    }
    public void CancelInteraction()
    {
        if (heldInteractable != null)
        {
            switch (heldInteractable)
            {
                case DeadBody D:
                    heldInteractable.Interact(gameObject);
                    heldInteractable = null;
                    player.DraggingBody = false;
                    break;

                case BloodSuckTarget B:
                    B.CancelSucking();
                    heldInteractable = null;
                    player.SuckingBlood = false;
                    break;
                default:
                    break;
            }
        }
        if (interactedBarrel != null)
        {
            Debug.Log("Leaving " + interactedBarrel.gameObject);
            player.Hiding = false;
            GetComponent<CharacterController>().enabled = true;
            interactedBarrel.Interact(gameObject);
            interactedBarrel = null;
        }
    }
}
