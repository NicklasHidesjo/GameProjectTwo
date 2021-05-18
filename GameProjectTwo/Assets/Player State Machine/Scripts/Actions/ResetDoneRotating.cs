using UnityEngine;

[CreateAssetMenu(fileName = "ResetDoneRotating", menuName = "Player/Action/ResetDoneRotating")]
public class ResetDoneRotating : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		player.DoneRotating = false;
	}
}
