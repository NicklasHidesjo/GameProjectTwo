using UnityEngine;

[CreateAssetMenu(fileName = "StopFleeingFromDeadBody", menuName = "AI/Decision/StopFleeingFromDeadBody")]
public class StopFleeingFromDeadBody : Decision
{
	public override bool Decide(ICharacter character)
	{
		if (character.Agent.velocity != Vector3.zero)
		{
			return false;
		}
		Vector3 direction = character.DeadNpc.transform.position - character.Transform.position;
		return !character.RayHitDeadNPC(direction, character.Stats.SightLenght);
	}
}
