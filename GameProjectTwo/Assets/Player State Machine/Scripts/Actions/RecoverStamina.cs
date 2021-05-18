using UnityEngine;

[CreateAssetMenu(fileName = "RecoverStamina", menuName = "Player/Action/RecoverStamina")]
public class RecoverStamina : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		player.RecoverStamina(player.Stats.StaminaRecovery);
	}
}
