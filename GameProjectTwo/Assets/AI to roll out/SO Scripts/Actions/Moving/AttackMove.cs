using UnityEngine;

[CreateAssetMenu(fileName = "AttackMove", menuName = "AI/Action/AttackMove")]
public class AttackMove : Action
{
	public override void Execute(ICharacter character)
	{
        if(character.StateTime < 1/character.Stats.AttacksPerSecond)
		{
			return;
		}
		character.Move(character.PlayerTransform.position);
	}
}
