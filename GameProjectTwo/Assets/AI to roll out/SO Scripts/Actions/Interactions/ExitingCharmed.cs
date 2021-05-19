using UnityEngine;

[CreateAssetMenu(fileName = "ExitingCharmed", menuName = "AI/Action/ExitingCharmed")]
public class ExitingCharmed : Action
{
	public override void Execute(ICharacter character)
	{
		PlayerStates currentState = character.Player.CurrentState;

        if(character.SeesPlayer && (currentState != PlayerStates.TransformToBat || currentState != PlayerStates.BatDefault))
		{
			character.SetAlertness(character.Stats.AlertActionThreshold);
		}
		else
		{
			character.SetAlertness(character.Stats.CautiousThreshold * 1.2f);
		}
		character.Player.CharmingTarget = false;
	}
}
