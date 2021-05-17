using UnityEngine;

[CreateAssetMenu(fileName = "SetPlayerSpeed", menuName = "Player/Action/SetPlayerSpeed")]
public class SetPlayerSpeed : PlayerAction
{
	[SerializeField] PlayerSpeeds speed;
	public override void Execute(IPlayer player)
	{
		switch (speed)
		{
			case PlayerSpeeds.Walk:
				player.Speed = player.Stats.WalkSpeed;
				break;
			case PlayerSpeeds.Run:
				player.Speed = player.Stats.RunSpeed;
				break;
			case PlayerSpeeds.DragBody:
				player.Speed = player.Stats.DragBodySpeed;
				break;
			case PlayerSpeeds.InSun:
				player.Speed = player.Stats.InSunSpeed;
				break;
			case PlayerSpeeds.Crouch:
				player.Speed = player.Stats.CrouchSpeed;
				break;
			case PlayerSpeeds.Fly:
				player.Speed = player.Stats.FlySpeed;
				break;
		}
	}
}
