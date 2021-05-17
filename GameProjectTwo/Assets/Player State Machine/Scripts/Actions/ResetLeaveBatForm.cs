using UnityEngine;

[CreateAssetMenu(fileName = "ResetLeaveBatForm", menuName = "Player/Action/ResetLeaveBatForm")]
public class ResetLeaveBatForm : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		player.LeaveBat = false;
	}
}
