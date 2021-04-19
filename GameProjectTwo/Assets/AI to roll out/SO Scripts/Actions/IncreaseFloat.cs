using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseFloat", menuName = "AI/Action/IncreaseFloat")]
public class IncreaseFloat : Action
{
	[SerializeField] NPCTimers floatToIncrease;

	public override void Execute(ICharacter character)
	{
		switch (floatToIncrease)
		{
			case NPCTimers.stateTime:
				character.StateTime += Time.fixedDeltaTime;
				break;
			case NPCTimers.lastAction:
				character.TimeSinceLastAction += Time.fixedDeltaTime;
				break;
			case NPCTimers.lastSeen:
				character.TimeSinceLastSeenPlayer += Time.fixedDeltaTime;
				break;
			default:
				break;
		}
	}
}
