using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableScanner))]
public class PlayerObjectInteract : MonoBehaviour
{

    InteractableScanner iScanner;
    Interactable interactable;
    Interactable heldInteractable;

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
            case DeadBody D:
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
            case Container C:
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
                    }
                }
                break;

            default:
                break;
        }
    }



    IEnumerator MoveTowardsPosition(Transform targetToMove, Vector3 targetPosition, float time)
    {
        //TODO Make Sure there is no player control during animation
        if (time == 0f)
        {
            time = 0.0001f;
        }
        Vector3 startPos = targetToMove.position;


        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            targetToMove.position = Vector3.Lerp(startPos, targetPosition, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

}