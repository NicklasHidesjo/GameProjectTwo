using UnityEngine;

[CreateAssetMenu(fileName = "PlayerTransform", menuName = "Player/Decision/PlayerTransform")]
public class PlayerTransform : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return Input.GetButtonDown("TransformShape");
	}
}
