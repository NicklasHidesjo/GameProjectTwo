using UnityEngine;

[CreateAssetMenu(fileName = "SetLastKnown", menuName = "AI/Action/SetLastKnown")]
public class SetLastKnown : Action
{
	public override void Execute(ICharacter character)
	{
		character.PlayerLastSeen = character.PlayerTransform.position;
	}
}
