using UnityEngine;

[CreateAssetMenu(fileName = "WalkToLastKnown", menuName = "AI/Action/WalkToLastKnown")]
public class WalkToLastKnown : Action
{
	public override void Execute(ICharacter character)
	{
		character.Move(character.LastKnown);
	}
}
