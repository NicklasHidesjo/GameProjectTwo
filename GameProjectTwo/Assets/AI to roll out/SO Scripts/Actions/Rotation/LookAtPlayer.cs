using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LookAtPlayer", menuName = "AI/Action/LookAtPlayer")]
public class LookAtPlayer : Action
{
	public override void Execute(ICharacter character)
	{
		if(!character.SeesPlayer && !character.NoticedPlayer)
		{
			return;
		}
		if (character.Alertness >= character.Stats.AlertActionThreshold)
		{
			character.LookAt(character.Player.position);
		}
		else
		{
			character.RotateTowardsPlayer();
		}

	}
}
