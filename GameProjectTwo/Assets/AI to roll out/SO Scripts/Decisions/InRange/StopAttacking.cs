using UnityEngine;

[CreateAssetMenu(fileName = "StopAttacking", menuName = "AI/Decision/StopAttacking")]
public class StopAttacking : Decision
{
	public override bool Decide(ICharacter character)
	{
		Vector3 direction = character.Player.position - character.Transform.position;
		float distance = character.Agent.stoppingDistance + 1f; // make this use a stat attack range
		return !character.RayHitTag("Player", character.Transform.forward, distance);
	}
}
