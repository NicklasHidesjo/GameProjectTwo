using UnityEngine;

[CreateAssetMenu(fileName = "LerpRotate", menuName = "AI/Action/LerpRotate")]
public class LerpRotate : Action
{
	public override void Execute(ICharacter character)
	{
		if(!character.RotationStarted) { return; }
		character.Transform.rotation = Quaternion.Slerp(character.OriginRot, character.TargetRot, character.RotationTime);
	}
}
