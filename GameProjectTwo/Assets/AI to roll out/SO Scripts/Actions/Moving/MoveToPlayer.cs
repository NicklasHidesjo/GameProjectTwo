using UnityEngine;

[CreateAssetMenu(fileName = "MoveToPlayer", menuName = "AI/Action/MoveToPlayer")]
public class MoveToPlayer : Action
{
	public override void Execute(ICharacter character)
	{
		character.Move(character.Player.position);
	}
}
