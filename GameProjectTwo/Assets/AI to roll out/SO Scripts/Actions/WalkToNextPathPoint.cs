using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkToNextPathPoint", menuName = "AI/Action/WalkToNextPathPoint")]
public class WalkToNextPathPoint : Action
{
	public override void Execute(ICharacter character)
	{
		character.Move(character.Path[character.PathIndex].position);
	}
}
