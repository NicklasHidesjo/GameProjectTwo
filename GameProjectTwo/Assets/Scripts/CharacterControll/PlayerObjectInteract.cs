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
    public OldPlayerState playerState;
    

    void Start()
    {
        iScanner = GetComponent<InteractableScanner>(); 
    }

    void Update()
    {

        if (Input.GetButtonDown("Interact"))
        {
            if (iScanner.CurrentInteractable == null)
            {
                return;
            }
            interactable = iScanner.CurrentInteractable;
            InteractWithObject();

        }

        //Cancel Button
        if (Input.GetButtonDown("Run"))
        {
            CancelInteraction();

        }

    }


    private void InteractWithObject()
    {
        // Switch to check if state is valid for interaction
        switch (playerState.CurrentState)
        {
            case PlayerStates.DraculaDefault:
                break;
            case PlayerStates.DraculaRunning:
                break;
            case PlayerStates.DraculaCrouching:
                break;
            case PlayerStates.DraculaDragBody:
                break;
            case PlayerStates.DraculaHidden:
                break;
            //case PlayerState.playerStates.DraculaSucking:
            //    break;
            default:
                    print(playerState.CurrentState + " state does not allow interaction");
                return;
        }

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
                    interactable.Interact(gameObject);
                    iScanner.RemoveInteractableFromList(heldInteractable);
                    playerState.SetState(PlayerStates.DraculaDragBody);
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
                        SetState(PlayerStates.DraculaDefault);
                    }
                    else
                    {
                        if (playerState.CurrentState == PlayerStates.DraculaHidden)
                        {
                            Debug.Log("Leaving " + C.gameObject);
                            playerState.SetState(PlayerStates.DraculaHideing);
                            GetComponent<CharacterController>().enabled = true;                        
                            interactable.Interact(gameObject);
                        }
                        else
                        {
                            Debug.Log("Entering " + C.gameObject);
                            GetComponent<CharacterController>().enabled = false;                           
                            interactable.Interact(gameObject);
                            playerState.SetState(PlayerStates.DraculaHideing);

                        }
                    }
                    break;
                }
            case BloodSuckTarget B: //see if the object itself can validate interaction
                {
                    CancelInteraction();
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
                    playerState.SetState(PlayerStates.DraculaDefault);
                    break;

                case BloodSuckTarget B:
                    B.CancelSucking();
                    heldInteractable = null;
                    playerState.SetState(PlayerStates.DraculaDefault);
                    break;

                default:
                    break;
            }

        }
    }

    public void SetState(PlayerStates newState)
    {
        playerState.SetState(newState);
    }




}
