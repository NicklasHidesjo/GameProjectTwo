using UnityEngine;

[CreateAssetMenu(fileName = "ResetStopHiding", menuName = "Player/Action/ResetStopHiding")]
public class ResetStopHiding : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		player.StopHiding = false;
	}
}
