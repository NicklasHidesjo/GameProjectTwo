using UnityEngine;

[CreateAssetMenu(fileName = "SetTargetRotation", menuName = "Player/Action/SetTargetRotation")]
public class SetTargetRotation : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		//float angle = Vector3.Angle(player.Transform., player.Interactable.transform.position);
		//Debug.Log(angle);
		player.originRot = player.Transform.rotation;
		Quaternion target = Quaternion.LookRotation(player.Interactable.transform.position - player.Transform.position);
		target.x = 0f;
		target.z = 0f;

		player.TargetRotation = target;
	}
}
