using UnityEngine;

[CreateAssetMenu(fileName = "StartAttacking", menuName = "AI/Decision/StartAttacking")]
public class StartAttacking : Decision
{
	public override bool Decide(ICharacter character)
	{
		float distance = Vector3.Distance(character.PlayerTransform.position, character.Transform.position);
		return distance < character.Agent.stoppingDistance + 1f;
	}
}
