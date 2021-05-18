using UnityEngine;

[CreateAssetMenu(fileName = "RotateTowardsTarget", menuName = "Player/Action/RotateTowardsTarget")]
public class RotateTowardsTarget : PlayerAction
{
	public override void Execute(IPlayer player)
	{
        if(player.DoneRotating)
		{
			return;
		}
	}
}
