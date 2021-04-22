using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shout", menuName = "AI/Action/Shout")]
public class Shout : Action
{
	public override void Execute(ICharacter character)
	{
		if (!character.ShouldShout) { return; }

		GameObject[] civilians = GameObject.FindGameObjectsWithTag("Civilian");
		GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard");

		foreach (var npc in civilians)
		{
			if(Vector3.Distance(character.Transform.position, npc.transform.position) < character.Stats.ShoutRange)
			{
				Debug.Log(npc.name + " Is hearing the shout");
				npc.GetComponent<ICharacter>().ReactToShout();
			}
		}
		foreach (var npc in guards)
		{
			if (Vector3.Distance(character.Transform.position, npc.transform.position) < character.Stats.ShoutRange)
			{
				npc.GetComponent<ICharacter>().ReactToShout();
			}
		}
	}
}
