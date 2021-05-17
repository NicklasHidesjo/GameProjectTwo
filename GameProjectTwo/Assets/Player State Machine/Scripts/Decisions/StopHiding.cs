using UnityEngine;

[CreateAssetMenu(fileName = "StopHiding", menuName = "Player/Decision/StopHiding")]
public class StopHiding : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return !player.Hiding;
	}
}
