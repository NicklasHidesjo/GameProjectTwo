using UnityEngine;

[CreateAssetMenu(fileName = "BloodRotate", menuName = "AI/Action/BloodRotate")]
public class BloodRotate : Action
{
	public override void Execute(ICharacter character)
	{
		Vector3 dir = character.PlayerTransform.position - character.Transform.position;
		float angle = Vector3.Angle(character.Transform.forward, dir);
		if(angle <= 90)
		{
			Debug.Log("sucked from the front");
			character.LookAt(character.PlayerTransform.position);
		}
		else
		{
			Debug.Log("Sucked from the back");
			Quaternion rotation = character.PlayerTransform.rotation;
			character.LookAt(rotation);
		}
	}
}
