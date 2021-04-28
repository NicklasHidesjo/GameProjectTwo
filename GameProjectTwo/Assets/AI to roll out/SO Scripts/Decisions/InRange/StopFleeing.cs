using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "StopFleeing", menuName = "AI/Decision/StopFleeing")]
public class StopFleeing : Decision
{
	public override bool Decide(ICharacter character)
	{
		if (character.Agent.velocity != Vector3.zero)
		{
			return false;
		}	
		if(character.NoticedPlayer) 
		{ 
			return false; 
		}
		Vector3 direction = character.Player.position - character.Transform.position;
		return !character.RayHitPlayer(direction, character.Stats.SightLenght);
	}
}
