using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStartRunning", menuName = "Player/Decision/PlayerStartRunning")]
public class PlayerStartRunning : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return Input.GetButton("Run");
	}
}
