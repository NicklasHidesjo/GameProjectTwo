using UnityEngine;

[CreateAssetMenu(fileName = "ResetLastKnown", menuName = "AI/Action/ResetLastKnown")]
public class ResetLastKnown : Action
{
	public override void Execute(ICharacter character)
	{
		character.PlayerLastSeen = character.Transform.position;
	}
}
