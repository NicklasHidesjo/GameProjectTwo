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
            attacker = player;
            StartCoroutine(SuckingBlood(10, 1f));
            player.GetComponent<PlayerObjectInteract>().SetState(PlayerState.playerStates.DraculaSucking);
            
        }
        else
        {
            print("Civilian not currently biteable");
        }
    }


    public void CancelSucking()
    {
        npcController.GettingSucked = false;
        StopCoroutine("SuckingBlood");        
        StopAllCoroutines();
        //Debug.Log("sucking Cancel!");
    }

    private IEnumerator SuckingBlood(int bloodPerSec, float tickRate)
    {
        PlayerStatsManager playerStats = PlayerManager.instance.gameObject.GetComponent<PlayerStatsManager>();
        AudioManager.instance.PlaySound(SoundType.DraculaBite);
        while (!npcController.IsDead)
        {
            //TODO Increase satiation!


            npcController.DecreaseHealth(bloodPerSec);
            //playerHealth.GainHealth(bloodPerSec);
            playerStats.IncreaseSatiationValue(bloodPerSec);
            playerStats.IncreaseHealthValue(bloodPerSec);
            Debug.Log("Currently Drinking!");
            yield return new WaitForSeconds(tickRate);
            AudioManager.instance.PlaySound(SoundType.DraculaDrink);

        }
        KillNPC();

        //Debug.Log("no longer sucking!");
        attacker.GetComponent<PlayerObjectInteract>().SetState(PlayerState.playerStates.DraculaDefault);
        AudioManager.instance.PlaySound(SoundType.DraculaDrinkDone);

    }
    
    //TODO: do this one correctly
    private void KillNPC()
    {
        //kill NPC

        AudioManager.instance.PlaySound(SoundType.CivilianDie, gameObject);
        GetComponent<Rigidbody>().isKinematic = false;
        npcController.Dead();
    }

}
