using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "StopFleeing", menuName = "AI/Decision/StopFleeing")]
public class StopFleeing : Decision
{
	public override bool Decide(ICharacter character)
	{
		if(character.Agent.remainingDistance > character.Agent.stoppingDistance) 
		{ 
			return false; 
		}

		// might be able to remove this (not yet checked)
		Vector3 dir = character.Player.position - character.Transform.position;
		Vector3 origin = character.Transform.position;
		RaycastHit hit;

		if (Physics.Raycast(origin, dir, out hit, character.Stats.SightLenght))
		{
			if (hit.collider.CompareTag("Player"))
			{
				return false;
			}
		}

		return true;
	}
}
