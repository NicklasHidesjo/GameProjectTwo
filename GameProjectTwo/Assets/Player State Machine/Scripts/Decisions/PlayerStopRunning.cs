using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStopRunning", menuName = "Player/Decision/PlayerStopRunning")]
public class PlayerStopRunning : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return !Input.GetButton("Run") || player.CurrentStamina <= 0;
	}
}
