using UnityEngine;

[CreateAssetMenu(fileName = "DrainStamina", menuName = "Player/Action/DrainStamina")]
public class DrainStamina : PlayerAction
{
	[SerializeField] StaminaCosts cost;
	public override void Execute(IPlayer player)
	{
		switch (cost)
		{
			case StaminaCosts.FlyCost:
				player.DecreaseStamina(player.Stats.FlyStaminaCost);
				break;
			case StaminaCosts.RunCost:
				if(player.Controller.velocity == Vector3.zero) { return; }
				player.DecreaseStamina(player.Stats.RunStaminaCost);
				break;
		}
	}
}
