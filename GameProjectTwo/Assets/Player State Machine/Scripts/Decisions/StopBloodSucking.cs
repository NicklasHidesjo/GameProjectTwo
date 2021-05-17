using UnityEngine;

[CreateAssetMenu(fileName = "StopBloodSucking", menuName = "Player/Decision/StopBloodSucking")]
public class StopBloodSucking : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return !player.SuckingBlood;
	}
}
