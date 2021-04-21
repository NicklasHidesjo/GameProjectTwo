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
		Vector3 direction = character.Player.position - character.Transform.position;
		return !character.RayHitTag("Player",direction , character.Stats.SightLenght);
	}
}