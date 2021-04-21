using UnityEngine;

[CreateAssetMenu(fileName = "UpdateSearchRotation", menuName = "AI/Action/UpdateSearchRotation")]
public class UpdateSearchRotation : Action
{
	public override void Execute(ICharacter character)
	{
		if(!character.RotationStarted) { return; }
		if(character.Transform.rotation != character.TargetRot) { return; }
		character.RotationTime = 0;

		character.RotationSpeed = character.Stats.RotationSpeed;
		character.OriginRot = character.Transform.rotation;

		float angle;
		if (character.YRotCorrection != 0)
		{
			Debug.Log(character.YRotCorrection);
			character.SearchAngle *= Random.Range(0, 2) * 2 - 1;
			angle = character.SearchAngle;
			character.YRotCorrection = 0;
		}
		else
		{
			character.SearchAngle *= -1;
			Debug.Log(character.SearchAngle);
			angle = character.SearchAngle * 2;
		}

		character.TargetRot = character.Transform.rotation * Quaternion.AngleAxis(angle, character.Transform.up);
	}
}
