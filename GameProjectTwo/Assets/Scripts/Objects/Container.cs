using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used as component to give ability to contain a GameObject like a player or a dead body
public class Container : Interactable
{

    private GameObject objectInside;
    public GameObject ObjectInside { get => objectInside; }

    public delegate void OnMoveCompleted();


    private bool playerInside = false;


    private void Start()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.blue, 999f);
        //standardMaterial = GetComponent<MeshRenderer>().material;
    }


    public override void Interact(GameObject obj)
    {

        Interactable isInteractable = obj.GetComponent<Interactable>();

        if (isInteractable != null)
        {
            Debug.Log("Placed " + obj + "in " + gameObject);
            AddToContainer(obj);
            obj.GetComponent<DeadBody>().SetHidden(true);

        }
        else if (obj.CompareTag("Player"))
        {
            Debug.Log("hiding in container");
            HideInContainer(obj);
        }

    }

    private void AddToContainer(GameObject thingToHide)
    {

        objectInside = thingToHide;
        //Physics.IgnoreCollision(thingToHide.GetComponent<Collider>(), GetComponent<Collider>());
        objectInside.GetComponent<Rigidbody>().isKinematic = true;
        thingToHide.GetComponent<Collider>().enabled = false;
        StartCoroutine(MoveTowardsPosition(thingToHide.transform, transform.position, 0.5f));
    }

    private void HideInContainer(GameObject player)
    {
        if (playerInside)
        {         
            playerInside = false;   
            StartCoroutine(MoveTowardsPosition(player.transform, gameObject.transform.position + gameObject.transform.forward * 2F, 0.3f, player.GetComponent<Player>()));
        }
        else
        {
            playerInside = true;           
            StartCoroutine(MoveTowardsPosition(player.transform, transform.position, 0.5f, player.GetComponent<Player>()));
        }
    }


    //To Move Objects
    IEnumerator MoveTowardsPosition(Transform targetToMove, Vector3 targetPosition, float time)
    {
        
        yield return new WaitForSeconds(0.3f);
        float startTime = Time.time;
        float journeyTime = time;
        
        Vector3 startPos = targetToMove.position;
        Vector3 center = startPos - Vector3.up;
        Vector3 startRelCenter = startPos - center;
        Vector3 endRelCenter = targetPosition - center;


        while (targetToMove.position != targetPosition)
        {
            float fracComplete = (Time.time - startTime) / journeyTime;
            targetToMove.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete);
            targetToMove.position += center;
            yield return null;
        }

        targetToMove.rotation = transform.rotation;

        //Debug.Log("finished moving");     
    }

    // To Move Player
    IEnumerator MoveTowardsPosition(Transform targetToMove, Vector3 targetPosition, float time, Player player)
    {
        targetToMove.LookAt(targetPosition);
        yield return new WaitForSeconds(time * 0.5f);
        float startTime = Time.time;
        float journeyTime = time;

        Vector3 startPos = targetToMove.position;
        Vector3 center = startPos - Vector3.up;
        Vector3 startRelCenter = startPos - center;
        Vector3 endRelCenter = targetPosition - center;

        while (targetToMove.position != targetPosition)
        {
            float fracComplete = (Time.time - startTime) / journeyTime;
            targetToMove.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete);
            targetToMove.position += center;
            yield return null;
        }
        //Debug.Log("finished moving");
        player.ContainerInteractionDone = true;
    }
}
