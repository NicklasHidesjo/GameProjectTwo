using UnityEngine;

[CreateAssetMenu(fileName = "MoveToDeadBody", menuName = "AI/Action/MoveToDeadBody")]
public class MoveToDeadBody : Action
{
	public override void Execute(ICharacter character)
	{
		if(character.DeadNpc.GettingDisposed)
		{
			character.DeadNpc = null;
			return;
		}
		character.Agent.destination = character.DeadNpc.transform.position;
	}
}
