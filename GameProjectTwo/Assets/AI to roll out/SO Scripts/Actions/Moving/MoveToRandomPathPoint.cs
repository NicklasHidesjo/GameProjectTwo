using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToRandomPathPoint", menuName = "AI/Action/MoveToRandomPathPoint")]
public class MoveToRandomPathPoint : Action
{
	public override void Execute(ICharacter character)
	{
		character.Move(character.Path[Random.Range(0, character.Path.Length)].position);
	}
}
