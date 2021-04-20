using UnityEngine;

[CreateAssetMenu(fileName = "SetBool", menuName = "AI/Action/SetBool")]
public class SetBool : Action
{
	[SerializeField] NPCBooleans setBool;
	[SerializeField] bool value;
	public override void Execute(ICharacter character)
	{
		switch (setBool)
		{
			case NPCBooleans.startedRotation:
				character.RotationStarted = value;
				break;
		}
	}
}
