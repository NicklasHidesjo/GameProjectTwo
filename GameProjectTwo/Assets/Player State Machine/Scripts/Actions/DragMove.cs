using UnityEngine;

[CreateAssetMenu(fileName = "DragMove", menuName = "Player/Action/DragMove")]
public class DragMove : PlayerAction
{
	public override void Execute(IPlayer player)
	{
        if(player.StateTime < player.Stats.InteractionTime)
		{
			return;
		}

		if(!player.DoneRotating)
		{
			return;
		}

		Vector3 playerVelocity =
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
