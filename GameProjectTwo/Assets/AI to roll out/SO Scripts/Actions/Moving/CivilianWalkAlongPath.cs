using UnityEngine;

[CreateAssetMenu(fileName = "CivilianWalkAlongPath", menuName = "AI/Action/CivilianWalkAlongPath")]
public class CivilianWalkAlongPath : Action
{
	public override void Execute(ICharacter character)
	{
		if (character.PathIndex >=  character.Path.Length)
		{
			character.Leave = true;
			return;
		}

		Vector3 point = character.Path[character.PathIndex].GetPosition();

		character.targetPoint = character.Path[character.PathIndex];

		character.Move(point);
	}
}
