using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LookAtPlayer", menuName = "AI/Action/LookAtPlayer")]
public class LookAtPlayer : Action
{
	public override void Execute(ICharacter character)
	{
		character.LookAt(character.Player.position);
	}
}
