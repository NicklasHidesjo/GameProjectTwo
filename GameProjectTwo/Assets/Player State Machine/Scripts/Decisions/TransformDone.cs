using UnityEngine;

[CreateAssetMenu(fileName = "TransformDone", menuName = "Player/Decision/TransformDone")]
public class TransformDone : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return player.StateTime > player.Stats.TransformDuration;
	}
}
