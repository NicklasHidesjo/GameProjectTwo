using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to scan the environment for objects inheriting from the Interactable class
//This script requires a triggercollider, I think spheres would be ideal
[RequireComponent(typeof(SphereCollider))]
public class InteractableScanner : MonoBehaviour
{

    [SerializeField] List<Interactable> interactables;
    [SerializeField] Interactable currentInteractable;
    public Interactable CurrentInteractable { get => currentInteractable;}

    [SerializeField] float interactRange;

    public float InteractRange { get => interactRange;}

    void Start()
    {
        interactables = new List<Interactable>();
    }

    
    void Update()
    {

        SetClosestInteractable();

    }

    private void SetClosestInteractable()
    {
        Interactable newInteractable = GetClosestInteractable();

        if (currentInteractable != null)
        {
            currentInteractable.SetSelected(true, this);
            if (newInteractable != currentInteractable)
            {
                currentInteractable.SetSelected(false, this);


            }
        }

        currentInteractable = newInteractable;
    }

    private Interactable GetClosestInteractable()
    {
        Interactable closestContainer = null;
        float closestDistance = interactRange;
        interactables.RemoveAll(Interactable => Interactable == null);
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
        if (interactables.Contains(newInteractable))
        {
            return;
        }
        interactables.Add(newInteractable);
        
    }

    public void RemoveInteractableFromList(Interactable newInteractable)
    {

        interactables.Remove(newInteractable);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Interactable i = other.GetComponent<Interactable>();
        
        if (i != null)
        {
            AddInteractableToList(i);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Interactable i = other.GetComponent<Interactable>();
        
        if (i != null)
        {
            RemoveInteractableFromList(i);
        }
    }

}
