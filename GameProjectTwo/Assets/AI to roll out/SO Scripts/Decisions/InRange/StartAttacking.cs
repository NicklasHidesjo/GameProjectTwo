using UnityEngine;

[CreateAssetMenu(fileName = "StartAttacking", menuName = "AI/Decision/StartAttacking")]
public class StartAttacking : Decision
{
	public override bool Decide(ICharacter character)
	{
		Vector3 direction = character.Player.position - character.Transform.position;
		float distance = character.Agent.stoppingDistance + 0.5f; // make this use a stat begin attack (attack range)

		return character.RayHitPlayer(direction, distance);
	}
}
