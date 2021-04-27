using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Used to interact with objects found with interactable scanner
[RequireComponent(typeof(InteractableScanner))]
public class PlayerObjectInteract : MonoBehaviour
{

    InteractableScanner iScanner;
    Interactable interactable;
    Interactable heldInteractable;
    public PlayerState playerState;
    bool tempHiddenState = false;

    void Start()
    {
        iScanner = GetComponent<InteractableScanner>(); 
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (iScanner.CurrentInteractable == null)
            {
                return;
            }
            interactable = iScanner.CurrentInteractable;
            InteractWithObject();

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heldInteractable != null)
            {
                heldInteractable.Interact(gameObject);
                Physics.IgnoreCollision(heldInteractable.GetComponent<Collider>(), GetComponent<SphereCollider>(), false);
                heldInteractable = null;
            }
            

        }

    }


    private void InteractWithObject()
    {

        switch (interactable)
        {
            case DeadBody D: //checks on player to see if interaction is valid
                if (heldInteractable != null)
                {
                    Debug.Log("Need to let go first dude: " + D.gameObject);
                    return;
                }
                Debug.Log("Interact dead dude: " + D.gameObject);
                heldInteractable = D;
                Physics.IgnoreCollision(heldInteractable.GetComponent<Collider>(), GetComponent<SphereCollider>());
                interactable.Interact(gameObject);
                iScanner.RemoveInteractableFromList(heldInteractable);
                break;
            case Container C: //checks on player to see if interaction is valid
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
                }
                else
                {
                    if (tempHiddenState)
                    {
                        Debug.Log("Leaving " + C.gameObject);

                        GetComponent<CharacterController>().enabled = true;
                        tempHiddenState = false;
                        interactable.Interact(gameObject);
                        

                    }
                    else
                    {
                        Debug.Log("Entering " + C.gameObject);

                        GetComponent<CharacterController>().enabled = false;
                        tempHiddenState = true;

                        interactable.Interact(gameObject);
                        playerState.SetState(PlayerState.playerStates.DraculaHidden);

                    }
                }
                break;
            case BloodSuckTarget B: //see if the object itself can validate interaction
                if (playerState.GetCurrentState() != PlayerState.playerStates.DraculaSucking)
                {
                    interactable.Interact(gameObject);
                    
                    transform.LookAt(interactable.transform);
                }
                else
                {
                    B.CancelSucking();
                    playerState.SetState(PlayerState.playerStates.DraculaDefault);

                }

                break;
            default:
                break;
        }
    }

    public void SetState(PlayerState.playerStates newState)
    {
        playerState.SetState(newState);
    }




}
