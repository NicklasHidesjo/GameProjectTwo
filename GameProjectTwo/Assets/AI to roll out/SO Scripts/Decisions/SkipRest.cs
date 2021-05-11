using UnityEngine;

[CreateAssetMenu(fileName = "SkipRest", menuName = "AI/Decision/SkipRest")]
public class SkipRest : Decision
{
	public override bool Decide(ICharacter character)
	{
		if (character.Agent.remainingDistance > character.Agent.stoppingDistance)
		{
			return false;
		}
		return !character.targetPoint.IdlePoint;
	}
}
