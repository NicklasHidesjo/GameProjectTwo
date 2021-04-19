using UnityEngine;

[CreateAssetMenu(fileName = "StartAttacking", menuName = "AI/Decision/StartAttacking")]
public class StartAttacking : Decision
{
	public override bool Decide(ICharacter character)
	{
		Vector3 dir = character.Player.position - character.Transform.position;
		RaycastHit hit;
		if (Physics.Raycast(character.Transform.position, dir, out hit, character.Agent.stoppingDistance +0.5f)) // make this use a stat begin attack
		{
			if (hit.collider.gameObject.CompareTag("Player"))
			{
				return true;
			}
		}
		return false;
	}
}
