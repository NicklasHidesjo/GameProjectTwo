using UnityEngine;

[CreateAssetMenu(fileName = "CivilianWalkAlongPath", menuName = "AI/Action/CivilianWalkAlongPath")]
public class CivilianWalkAlongPath : Action
{
	public override void Execute(ICharacter character)
	{
		if (character.PathIndex >=  character.Path.Count)
		{
			character.Leave = true;
			return;
		}

		PathPoint pathPoint = character.Path[character.PathIndex];

		character.targetPoint = pathPoint;

		character.Move(pathPoint.Position);
	}
}
