using UnityEngine;

[CreateAssetMenu(fileName = "StartBloodSucking", menuName = "Player/Decision/StartBloodSucking")]
public class StartBloodSucking : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return player.SuckingBlood;
	}
}
