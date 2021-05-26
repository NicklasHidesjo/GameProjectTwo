using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to scan the environment for objects inheriting from the Interactable class

public class InteractableScanner : MonoBehaviour
{
    [SerializeField] HashSet<Interactable> interactables;
    [SerializeField] Interactable currentInteractable;
    public Interactable CurrentInteractable { get => currentInteractable;}

    [SerializeField] float interactRange;
    [SerializeField] LayerMask layersToSearch;

    Player player;

    void Start()
    {
        player = GetComponent<Player>();
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
            currentInteractable.SetSelected(false, this);
            currentInteractable = null;
        }

        currentInteractable = newInteractable;

        if(currentInteractable != null)
		{
			if (UnInteractableState())
			{
				return;
			}

			currentInteractable.SetSelected(true, this);
		}
	}

	private bool UnInteractableState()
	{
		return player.CurrentState == PlayerStates.DraculaSucking ||
			   player.CurrentState == PlayerStates.BatDefault ||
			   player.CurrentState == PlayerStates.TransformToBat || 
               player.CurrentState == PlayerStates.DraculaRunning;
	}

	private Interactable GetClosestInteractable()
    {
        Interactable closestContainer = null;
        float closestDistance = interactRange;
        
        foreach (Interactable i in interactables)
        {
            if(i.GetComponent<NPC>() != null)
			{
                if(i.GetComponent<NPC>().CurrentState == NPCStates.LeaveState)
				{
                    continue;
				}
			}
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
