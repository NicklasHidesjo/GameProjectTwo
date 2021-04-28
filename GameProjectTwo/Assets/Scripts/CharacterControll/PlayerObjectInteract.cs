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

            case PlayerState.playerStates.DraculaDefault:
                break;
            case PlayerState.playerStates.DraculaRunning:
                break;
            case PlayerState.playerStates.DraculaCrouching:
                break;
            case PlayerState.playerStates.DraculaDragBody:
                break;
            case PlayerState.playerStates.DraculaHidden:
                break;
            case PlayerState.playerStates.DraculaSucking:
                break;
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
                        Debug.Log("Need to let go first dude: " + D.gameObject);
                        return;
                    }
                    Debug.Log("Interact dead dude: " + D.gameObject);
                    heldInteractable = D;
                    interactable.Interact(gameObject);
                    iScanner.RemoveInteractableFromList(heldInteractable);
                    playerState.SetState(PlayerState.playerStates.DraculaDragBody);
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
                    }
                    else
                    {
                        if (playerState.CurrentState == PlayerState.playerStates.DraculaHidden)
                        {

                            Debug.Log("Leaving " + C.gameObject);
                            playerState.SetState(PlayerState.playerStates.DraculaHideing);
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
                            playerState.SetState(PlayerState.playerStates.DraculaHideing);

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
                    playerState.SetState(PlayerState.playerStates.DraculaDefault);
                    break;

                case BloodSuckTarget B:
                    B.CancelSucking();
                    heldInteractable = null;
                    playerState.SetState(PlayerState.playerStates.DraculaDefault);
                    break;

                default:
                    break;
            }

        }
    }

    public void SetState(PlayerState.playerStates newState)
    {
        playerState.SetState(newState);
    }




}
