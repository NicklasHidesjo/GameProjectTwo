using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to scan the environment for objects inheriting from the Interactable class
//This script requires a triggercollider, I think spheres would be ideal
public class InteractableScanner : MonoBehaviour
{

    [SerializeField] HashSet<Interactable> interactables;
    [SerializeField] Interactable currentInteractable;
    public Interactable CurrentInteractable { get => currentInteractable;}

    [SerializeField] float interactRange;

    public float InteractRange { get => interactRange;}
    [SerializeField] LayerMask layersToSearch;

    void Start()
    {
        interactables = new HashSet<Interactable>();
    }

    private void FixedUpdate()
    {
        GetInteractablesInRange();
        SetClosestInteractable();
    }

    private void GetInteractablesInRange()
    {

        interactables.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange, layersToSearch);

        for (int i = 0; i < colliders.Length; i++)
        {
            Interactable test = colliders[i].GetComponent<Interactable>();

            if (test != null)
            {
                AddInteractableToList(test);
            }
        }
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


}
