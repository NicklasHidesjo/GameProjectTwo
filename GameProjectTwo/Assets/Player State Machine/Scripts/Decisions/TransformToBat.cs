using UnityEngine;

[CreateAssetMenu(fileName = "TransformToBat", menuName = "Player/Decision/TransformToBat")]
public class TransformToBat : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return Input.GetButtonDown("TransformShape") && player.CurrentStamina > player.Stats.FlyStaminaCost;
	}
}
