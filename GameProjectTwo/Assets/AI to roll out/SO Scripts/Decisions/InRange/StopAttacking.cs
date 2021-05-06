using UnityEngine;

[CreateAssetMenu(fileName = "StopAttacking", menuName = "AI/Decision/StopAttacking")]
public class StopAttacking : Decision
{
	[SerializeField] LayerMask mask;
	public override bool Decide(ICharacter character)
	{
		/*		Vector3 direction = character.Player.position - character.Transform.position;
				float distance = character.Agent.stoppingDistance + 1f; // make this use a stat attack range
				return !character.RayHitTarget(character.Transform.forward, distance,mask);*/

		float distance = Vector3.Distance(character.Player.position, character.Transform.position);
		return distance > character.Agent.stoppingDistance + 1f;
	}
}
