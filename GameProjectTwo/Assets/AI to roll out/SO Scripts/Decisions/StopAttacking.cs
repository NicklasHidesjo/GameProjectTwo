using UnityEngine;

[CreateAssetMenu(fileName = "StopAttacking", menuName = "AI/Decision/StopAttacking")]
public class StopAttacking : Decision
{
	public override bool Decide(ICharacter character)
	{
		Vector3 dir = character.Player.position - character.Transform.position;
		RaycastHit hit;
		if (Physics.Raycast(character.Transform.position, dir, out hit, character.Agent.stoppingDistance + 0.5f)) // make this use a stat attack range
		{
			if (hit.collider.gameObject.CompareTag("Player"))
			{
				return false;
			}
		}
		return true;
	}
}
