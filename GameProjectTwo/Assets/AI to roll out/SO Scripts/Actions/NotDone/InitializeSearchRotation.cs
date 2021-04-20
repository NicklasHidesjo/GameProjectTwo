using UnityEngine;

[CreateAssetMenu(fileName = "InitializeSearchRotation", menuName = "AI/Action/InitializeSearchRotation")]
public class InitializeSearchRotation : Action
{
	public override void Execute(ICharacter character)
	{
		if(character.RotationStarted) { return; }
		if(character.Agent.velocity != Vector3.zero) { return; }
		character.Agent.updateRotation = false;
		Debug.Log("initializing rotation");
		if (character.YRotCorrection != 0)
		{
			character.TargetRot = character.Transform.rotation * Quaternion.AngleAxis(character.YRotCorrection, character.Transform.up);
		}
		else
		{
			character.SearchAngle *= Random.Range(0, 2) * 2 - 1;
			Debug.Log(character.SearchAngle);
			float angle = character.SearchAngle;
			character.TargetRot = character.Transform.rotation * Quaternion.AngleAxis(angle, character.Transform.up);
		}

		character.OriginRot = character.Transform.rotation;
		character.RotationSpeed = character.RotationSpeed * 2;
		character.RotationStarted = true;
	}
}
