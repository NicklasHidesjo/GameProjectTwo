using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveBackwards", menuName = "AI/Action/MoveBackwards")]
public class MoveBackwards : Action
{
	public override void Execute(ICharacter character)
	{
		character.Move(character.Transform.position);
		if(character.Alertness < character.Stats.AlertActionThreshold)
		{
			return;
		}
		if (!character.SeesPlayer)
		{
			return;
		}
		Vector3 newPos = character.Transform.position - (character.Transform.forward * 2);
		character.Move(newPos);
	}
}
