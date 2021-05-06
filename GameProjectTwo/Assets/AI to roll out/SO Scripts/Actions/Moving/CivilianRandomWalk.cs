using UnityEngine;

[CreateAssetMenu(fileName = "CivilianRandomWalk", menuName = "AI/Action/CivilianRandomWalk")]
public class CivilianRandomWalk : Action
{
	public override void Execute(ICharacter character)
	{
		if(!character.WalkRandomly)
		{
			return;
		}
		if (character.Path.Count < 1)
		{
			character.Leave = true;
			return;
		}

		Transform pathPoint = character.Path[Random.Range(0, character.Path.Count)];
		character.Move(pathPoint.position);
		character.Path.Remove(pathPoint);
		character.Path.RemoveAll(path => path == null);
	}
}
