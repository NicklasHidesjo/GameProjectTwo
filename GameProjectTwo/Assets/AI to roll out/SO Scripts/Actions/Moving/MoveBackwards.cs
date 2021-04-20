using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveBackwards", menuName = "AI/Action/MoveBackwards")]
public class MoveBackwards : Action
{
	public override void Execute(ICharacter character)
	{
		Vector3 newPos = character.Transform.position - (character.Transform.forward * 10);
		character.Move(newPos);
	}
}
