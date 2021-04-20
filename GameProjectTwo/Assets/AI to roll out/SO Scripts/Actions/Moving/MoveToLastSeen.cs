using UnityEngine;

[CreateAssetMenu(fileName = "MoveToLastSeen", menuName = "AI/Action/MoveToLastSeen")]
public class MoveToLastSeen : Action
{
	public override void Execute(ICharacter character)
	{
		character.Move(character.PlayerLastSeen);
	}
}
