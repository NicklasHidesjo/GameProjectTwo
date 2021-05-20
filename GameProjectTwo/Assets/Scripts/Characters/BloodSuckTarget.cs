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
        StopCoroutine("SuckingBlood");        
        StopAllCoroutines();
        npcController.Dead();
    }

    private IEnumerator SuckingBlood(int bloodPerSec, float tickRate)
    {
        PlayerStatsManager playerStats = player.GetComponent<PlayerStatsManager>();
        AudioManager.instance.PlaySound(SoundType.DraculaBite);
        while (npcController.CurrentHealth > 0)
        {
            npcController.DecreaseHealth(bloodPerSec);
            playerStats.IncreaseSatiationValue(bloodPerSec);
            playerStats.IncreaseHealthValue(bloodPerSec);
            Debug.Log("Currently Drinking!");
            yield return new WaitForSeconds(tickRate);
            AudioManager.instance.PlaySound(SoundType.DraculaDrink);
        }
        player.SuckingBlood = false;
        AudioManager.instance.PlaySound(SoundType.DraculaDrinkDone);
        npcController.Dead();
    }
}
