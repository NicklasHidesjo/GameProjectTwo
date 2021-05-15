using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//used to allow for blood and health to be siphoned to the player
public class BloodSuckTarget : Interactable
{
    private NPC npcController;
    private Player player;

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
            this.player = player.GetComponent<Player>();
            this.player.SuckingBlood = true;
            StartCoroutine(SuckingBlood(10, 1f));
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
        PlayerStatsManager playerStats = player.GetComponent<PlayerStatsManager>();
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
        player.SuckingBlood = false;
        KillNPC();
        //Debug.Log("no longer sucking!");
        AudioManager.instance.PlaySound(SoundType.DraculaDrinkDone);
        npcController.RemoveBloodSuckTarget();
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
