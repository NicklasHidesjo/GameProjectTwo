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
            //Debug.Log("Placed " + obj + "in " + gameObject);
            AddToContainer(obj);
            obj.GetComponent<DeadBody>().SetHidden(true);

        }
        else if (obj.CompareTag("Player"))
        {
            HideInContainer(obj);
        }
        //TODO call playsound on the animation for better timing
        AudioManager.instance.PlayOneShot(SoundType.HideInContainer, gameObject);

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
            playerInside = false;   
            StartCoroutine(MoveTowardsPosition(player.transform, gameObject.transform.position + gameObject.transform.forward * 2F, 1f, PlayerState.playerStates.DraculaDefault));

        }
        else
        {
            playerInside = true;           
            StartCoroutine(MoveTowardsPosition(player.transform, transform.position, 1f, PlayerState.playerStates.DraculaHidden));
            print(transform.position);
        }
    }


    //To Move Objects
    IEnumerator MoveTowardsPosition(Transform targetToMove, Vector3 targetPosition, float time)
    {
        
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

    // To Move Player
    IEnumerator MoveTowardsPosition(Transform targetToMove, Vector3 targetPosition, float time, PlayerState.playerStates state)
    {
        
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
        //player.GetComponent<PlayerObjectInteract>().SetState(PlayerState.playerStates.DraculaDefault);
        SetState(state);
    }

    public void SetState(PlayerState.playerStates newState)
    {
        PlayerManager.instance.GetComponent<PlayerState>().SetState(newState);       
    }

}
