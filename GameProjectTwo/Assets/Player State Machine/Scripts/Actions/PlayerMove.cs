using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMove", menuName = "Player/Action/PlayerMove")]
public class PlayerMove : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		Vector3 playerVelocity  =
			Input.GetAxis("Horizontal") * FlatAlignTo(player.AlignCamera.right) +
			Input.GetAxis("Vertical") * FlatAlignTo(player.AlignCamera.forward);

		playerVelocity.Normalize();

		playerVelocity *= player.Speed;

		Vector3 forwardFromMovement = playerVelocity;
		forwardFromMovement.y = 0;

		if (forwardFromMovement.sqrMagnitude > 0)
		{
			player.Transform.forward = forwardFromMovement;
		}

		playerVelocity.y -= player.Stats.Gravity;

		player.Controller.Move(playerVelocity * Time.deltaTime);
	}

	Vector3 FlatAlignTo(Vector3 v)
	{
		v.y = 0;
		return v.normalized;
	}
}
