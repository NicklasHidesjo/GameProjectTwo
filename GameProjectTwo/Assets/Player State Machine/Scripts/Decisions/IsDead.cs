using UnityEngine;

[CreateAssetMenu(fileName = "IsDead", menuName = "Player/Decision/IsDead")]
public class IsDead : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return player.IsDead;
	}
}
