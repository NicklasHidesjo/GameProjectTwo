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

		
		player.Transform.rotation = Quaternion.Slerp(player.originRot, player.TargetRotation, player.StateTime * 2);

		if(player.StateTime *2 > 1)
		{
			player.DoneRotating = true;
		}

	}
}
