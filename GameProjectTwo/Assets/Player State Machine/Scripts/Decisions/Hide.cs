using UnityEngine;

[CreateAssetMenu(fileName = "Hide", menuName = "Player/Decision/Hide")]
public class Hide : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return player.Hidding;
	}
}
