using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToNextPathPoint", menuName = "AI/Action/MoveToNextPathPoint")]
public class MoveToNextPathPoint : Action
{
	public override void Execute(ICharacter character)
	{
		character.Move(character.Path[character.PathIndex].position);
	}
}
