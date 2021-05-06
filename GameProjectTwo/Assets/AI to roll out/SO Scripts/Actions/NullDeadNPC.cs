using UnityEngine;

[CreateAssetMenu(fileName = "NullDeadNPC", menuName = "AI/Action/NullDeadNPC")]
public class NullDeadNPC : Action
{
	public override void Execute(ICharacter character)
	{
		character.DeadNpc = null;
	}
}
