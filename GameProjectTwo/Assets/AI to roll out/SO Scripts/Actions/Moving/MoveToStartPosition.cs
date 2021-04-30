using UnityEngine;

[CreateAssetMenu(fileName = "MoveToStartPosition", menuName = "AI/Action/MoveToStartPosition")]
public class MoveToStartPosition : Action
{
	public override void Execute(ICharacter character)
	{
		character.Agent.destination = character.StartingPosition;
	}
}
