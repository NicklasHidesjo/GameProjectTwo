using UnityEngine;

[CreateAssetMenu(fileName = "WalkToPlayer", menuName = "AI/Action/WalkToPlayer")]
public class WalkToPlayer : Action
{
	public override void Execute(ICharacter character)
	{
		character.Move(character.Player.position);
	}
}
