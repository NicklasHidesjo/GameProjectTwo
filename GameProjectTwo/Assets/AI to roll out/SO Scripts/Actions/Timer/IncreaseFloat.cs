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
				if (character.SeesPlayer){ break; }
				character.TimeSinceLastSeenPlayer += Time.fixedDeltaTime;
				break;
			case NPCTimers.rotationTime:
				if(!character.RotationStarted) { break; }
				character.RotationTime += Mathf.Lerp(0, 1f, (character.Stats.RotationSpeed * 0.01f) * Time.fixedDeltaTime);
				break;
		}
	}
}
