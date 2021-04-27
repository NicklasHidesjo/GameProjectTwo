using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shout", menuName = "AI/Action/Shout")]
public class Shout : Action
{
	public override void Execute(ICharacter character)
	{
		if (!character.ShouldShout) { return; }

		foreach (var npc in character.NearbyCharacters)
		{
			if (npc == null) continue;
			npc.ReactToShout();
		}
	}
}
