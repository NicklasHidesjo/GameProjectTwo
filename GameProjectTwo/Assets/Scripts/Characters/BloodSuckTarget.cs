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
            StartCoroutine(SuckingBlood(10, 1f));
            player.GetComponent<PlayerObjectInteract>().SetState(PlayerState.playerStates.Sucking);
        }
        else
        {
            print("target not currently suckable");
        }
    }


    public void CancelSucking()
    {
        npcController.GettingSucked = false;
        StopCoroutine("SuckingBlood");        
        StopAllCoroutines();
        Debug.Log("sucking Cancel!");
    }

    private IEnumerator SuckingBlood(int bloodPerSec, float tickRate)
    {
        PlayerStatsManager playerStats = PlayerManager.instance.gameObject.GetComponent<PlayerStatsManager>();

        while (!npcController.IsDead)
        {
            //TODO Increase satiation!


            npcController.DecreaseHealth(bloodPerSec);
            //playerHealth.GainHealth(bloodPerSec);
            playerStats.IncreaseSatiationValue(bloodPerSec);
            playerStats.IncreaseHealthValue(bloodPerSec);
            Debug.Log("Currently sucking!");
            yield return new WaitForSeconds(tickRate);

        }
        KillNPC();

        Debug.Log("no longer sucking!");
        attacker.GetComponent<PlayerObjectInteract>().SetState(PlayerState.playerStates.MoveDracula);

    }
    private void KillNPC()
    {
        //kill NPC
        //player.GetComponent<InteractableScanner>().RemoveInteractableFromList(this);
        GetComponent<Rigidbody>().isKinematic = false;
        gameObject.AddComponent<DeadBody>();
        Destroy(GetComponent<SphereCollider>());
        Destroy(GetComponent<StateMachine>());
        Destroy(GetComponent<NavMeshAgent>());
        Destroy(this);
    }

}
