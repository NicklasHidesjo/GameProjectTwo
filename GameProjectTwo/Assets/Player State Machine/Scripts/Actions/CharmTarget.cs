using UnityEngine;

[CreateAssetMenu(fileName = "CharmTarget", menuName = "Player/Action/CharmTarget")]
public class CharmTarget : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		if(player.charmTarget == null)
		{
			return;
		}
		if(player.CharmingTarget)
		{
			return;
		}
		if(player.CurrentStamina < player.Stats.CharmStaminaCost)
		{
			return;
		}
        if(Input.GetButtonDown("Charm"))
		{
			player.DecreaseStamina(player.Stats.CharmStaminaCost);
			player.charmTarget.IsCharmed = true;
			player.CharmingTarget = true;
		}
	}
}
