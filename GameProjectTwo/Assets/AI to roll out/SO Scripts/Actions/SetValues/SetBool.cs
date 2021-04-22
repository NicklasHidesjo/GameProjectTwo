using UnityEngine;

[CreateAssetMenu(fileName = "SetBool", menuName = "AI/Action/SetBool")]
public class SetBool : Action
{
	[SerializeField] NPCBooleans setBool;
	[SerializeField] bool isTrue;
	public override void Execute(ICharacter character)
	{
		switch (setBool)
		{
			case NPCBooleans.StartedRotation:
				character.RotationStarted = value;
				break;
			case NPCBooleans.ShouldShout:
				character.ShouldShout = value;
				break;
			case NPCBooleans.IsSuckable:
				character.IsSuckable = value;
				break;
		}
	}
}
