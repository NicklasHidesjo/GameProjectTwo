using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//used to allow for blood and health to be siphoned to the player
public class BloodSuckTarget : Interactable
{
    private NPC npcController;
    private GameObject attacker;

    void Start()
    {
        standardMaterial = GetComponent<MeshRenderer>().material;
        npcController = GetComponent<NPC>();
    }

    public override void Interact(GameObject player)
    {
        if (npcController.IsSuckable)
        {
            npcController.GettingSucked = true;
            this.attacker = player;
            StartCoroutine(SuckingBlood(10, player.GetComponent<HealthManager>(), 1f));
            
        }
    }

    private void KillNPC()
    {
        //kill NPC
        //player.GetComponent<InteractableScanner>().RemoveInteractableFromList(this);
        GetComponent<Rigidbody>().isKinematic = false;
        gameObject.AddComponent<DeadBody>();
        Destroy(GetComponent<StateMachine>());
        Destroy(GetComponent<NavMeshAgent>());
        Destroy(this);
    }

    public void CancelSucking()
    {
        npcController.GettingSucked = false;
        StopCoroutine("SuckingBlood");        //TODO Check if this works
        StopAllCoroutines();
        Debug.Log("sucking Cancel!");
    }

    private IEnumerator SuckingBlood(int bloodPerSec, HealthManager playerHealth, float tickRate)
    {
        
        while (!npcController.IsDead)
        {
            npcController.DecreaseHealth(bloodPerSec);
            playerHealth.GainHealth(bloodPerSec);
            Debug.Log("Currently sucking!");
            yield return new WaitForSeconds(tickRate);

        }
        KillNPC();

        Debug.Log("no longer sucking!");
        attacker.GetComponent<PlayerObjectInteract>().SetState(PlayerState.playerStates.MoveDracula);

    }

}
