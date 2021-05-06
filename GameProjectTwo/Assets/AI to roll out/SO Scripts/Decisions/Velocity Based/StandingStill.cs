using UnityEngine;

[CreateAssetMenu(fileName = "StandingStill", menuName = "AI/Decision/StandingStill")]
public class StandingStill : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.Agent.velocity == Vector3.zero;
	}
}
