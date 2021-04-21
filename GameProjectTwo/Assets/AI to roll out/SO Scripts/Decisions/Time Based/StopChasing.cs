using UnityEngine;

[CreateAssetMenu(fileName = "StopChasing", menuName = "AI/Decision/StopChasing")]
public class StopChasing : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.TimeSinceLastSeenPlayer > character.Stats.IntuitionTime;
	}
}
