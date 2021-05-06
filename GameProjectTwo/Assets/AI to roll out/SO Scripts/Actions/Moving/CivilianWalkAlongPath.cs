using UnityEngine;

[CreateAssetMenu(fileName = "CivilianWalkAlongPath", menuName = "AI/Action/CivilianWalkAlongPath")]
public class CivilianWalkAlongPath : Action
{
	public override void Execute(ICharacter character)
	{
		if (character.WalkRandomly)
		{
			return;
		}
		if (character.Path.Count < 1)
		{
			character.Leave = true;
			return;
		}

		Transform pathPoint;
		if (character.Increase)
		{
			pathPoint = character.Path[0];
		}
		else
		{
			pathPoint = character.Path[character.Path.Count-1];
		}

		character.Move(pathPoint.position);
		character.Path.Remove(pathPoint);
		character.Path.RemoveAll(path => path == null);
	}
}
