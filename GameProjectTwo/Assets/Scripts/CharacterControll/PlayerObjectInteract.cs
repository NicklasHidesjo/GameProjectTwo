using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerObjectInteract : MonoBehaviour
{

    [SerializeField] List<Interactable> interactables;
    [SerializeField] Interactable interactable;

    private float interactRange;
    private bool hiddenInBox = false;

    public float InteractRange { get => interactRange;}

    void Start()
    {
        interactables = new List<Interactable>();
    }

    
    void Update()
    {
        SetClosestInteractable();
        if (Input.GetKeyDown(KeyCode.E) && interactable != null)
        {
            if (hiddenInBox)
            {
                Physics.IgnoreCollision(interactable.GetComponent<MeshCollider>(), GetComponent<CapsuleCollider>(), false);
                hiddenInBox = false;
                StartCoroutine(MoveTowardsPosition(transform.position + transform.forward, 1f));
            }
            else
            {
                hiddenInBox = true;
                Physics.IgnoreCollision(interactable.GetComponent<MeshCollider>(), GetComponent<CapsuleCollider>());
                StartCoroutine(MoveTowardsPosition(interactable.transform.position, 1f));

            }
        }

    }

    IEnumerator MoveTowardsPosition(Vector3 targetPos, float time)
    {
        //TODO Make Sure there is no player control during animation
        if (time == 0f)
        {
            time = 0.0001f;
        }
        Vector3 startPos = transform.position;

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }


    private void SetClosestInteractable()
    {
        Interactable newInteractable = GetClosestInteractable();

        if (interactable != null)
        {
            interactable.SetSelected(true);
            if (newInteractable != interactable)
            {
                interactable.SetSelected(false);


            }
        }

        interactable = newInteractable;
    }

    private Interactable GetClosestInteractable()
    {
        Interactable closestContainer = null;
        float closestDistance = Mathf.Infinity;

        foreach (Interactable i in interactables)
        {
            Vector3 directionToTarget = i.transform.position - transform.position;
            float directionSqrToTarget = directionToTarget.sqrMagnitude;
            if (directionSqrToTarget < closestDistance)
            {
                closestDistance = directionSqrToTarget;
                closestContainer = i;
            }
        }
        
        return closestContainer;
    }

    public void AddInteractableToList(Interactable newInteractable)
    {
        interactables.Add(newInteractable);
        
    }

    public void RemoveInteractableFromList(Interactable newInteractable)
    {

        interactables.Remove(newInteractable);
    }

    private void OnTriggerEnter(Collider other)
    {

        Interactable container = other.GetComponent<Interactable>();
        
        if (container != null)
        {
            AddInteractableToList(container);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Interactable container = other.GetComponent<Interactable>();
        
        if (container != null)
        {
            RemoveInteractableFromList(container);
        }
    }

}
