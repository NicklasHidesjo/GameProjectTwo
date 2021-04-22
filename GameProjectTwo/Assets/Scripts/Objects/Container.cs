using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used as component to give ability to contain a GameObject like a player or a dead body
public class Container : Interactable
{

    GameObject objectInside;
    public GameObject ObjectInside { get => objectInside; }

    private bool playerInside = false;


    private void Start()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.blue, 999f);
        standardMaterial = GetComponent<MeshRenderer>().material;
    }


    public override void Interact(GameObject obj)
    {

        Interactable isInteractable = obj.GetComponent<Interactable>();

        if (isInteractable != null)
        {
            Debug.Log("Placed " + obj + "in " + gameObject);
            AddToContainer(obj);

        }
        else if (obj.CompareTag("Player"))
        {
            HideInContainer(obj);
        }

    }

    private void AddToContainer(GameObject thingToHide)
    {

        objectInside = thingToHide;
        //Physics.IgnoreCollision(thingToHide.GetComponent<Collider>(), GetComponent<Collider>());
        objectInside.GetComponent<Rigidbody>().isKinematic = true;
        thingToHide.GetComponent<Collider>().enabled = false;
        StartCoroutine(MoveTowardsPosition(thingToHide.transform, transform.position, 1f));
        

    }

    private void HideInContainer(GameObject player)
    {
        if (playerInside)
        {
            //Physics.IgnoreCollision(player.GetComponent<CapsuleCollider>(), GetComponent<Collider>(), false);
            playerInside = false;
            StartCoroutine(MoveTowardsPosition2(player.transform, gameObject.transform.position + gameObject.transform.forward * 2F, 1f, player));

        }
        else
        {

            playerInside = true;
            Physics.IgnoreCollision(player.GetComponent<CapsuleCollider>(), GetComponent<Collider>());
            StartCoroutine(MoveTowardsPosition(player.transform, transform.position, 1f));
        }
    }



    IEnumerator MoveTowardsPosition(Transform targetToMove, Vector3 targetPosition, float time)
    {
        //TODO Make Sure there is no player control during animation!!!
        // Make sure playerstate allows external position manpulation when calling this
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
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("finished moving");
        
    }
    IEnumerator MoveTowardsPosition2(Transform targetToMove, Vector3 targetPosition, float time, GameObject player)
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
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("finished moving");
        player.GetComponent<PlayerObjectInteract>().SetState(PlayerState.playerStates.MoveDracula);
    }

}
