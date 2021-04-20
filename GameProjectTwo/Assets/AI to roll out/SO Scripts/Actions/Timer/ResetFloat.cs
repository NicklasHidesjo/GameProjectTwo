using UnityEngine;

[CreateAssetMenu(fileName = "ResetFloat", menuName = "AI/Action/ResetFloat")]
public class ResetFloat : Action
{
	[SerializeField] NPCTimers timerToReset;
	public override void Execute(ICharacter character)
	{
		switch (timerToReset)
		{
			case NPCTimers.stateTime:
				character.StateTime = 0;
				break;
			case NPCTimers.lastAction:
				character.TimeSinceLastAction = 0;
				break;
			case NPCTimers.lastSeen:
				character.TimeSinceLastSeenPlayer = 0;
				break;
			case NPCTimers.rotationTime:
				character.RotationTime = 0;
				break;
		}
	}
}
