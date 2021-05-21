using UnityEngine;

[CreateAssetMenu(fileName = "SetStartingRotation", menuName = "AI/Action/SetStartingRotation")]
public class SetStartingRotation : Action
{
	public override void Execute(ICharacter character)
	{
		if (character.Agent.remainingDistance > character.Agent.stoppingDistance)
		{
			return;
		}
		if(character.RotationStarted)
		{
			return;
		}


		character.YRotCorrection = 1;
		character.RotationTime = 0;

		character.OriginRot = character.Transform.rotation;
		character.TargetRot = character.StartingRotation;

		character.RotationSpeed = character.RotationSpeed * 2;

		character.Agent.updateRotation = false;
		character.RotationStarted = true; 
	}
}
