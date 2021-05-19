using UnityEngine;

[CreateAssetMenu(fileName = "ExitingCharmed", menuName = "AI/Action/ExitingCharmed")]
public class ExitingCharmed : Action
{
	public override void Execute(ICharacter character)
	{
		PlayerStates currentState = character.Player.CurrentState;

        if(character.SeesPlayer && (currentState != PlayerStates.TransformToBat || currentState != PlayerStates.BatDefault))
		{
			character.SetAlertnessToMax();
		}
		else
		{
			character.SetAlertness(character.Stats.ExitCharmIncrease);
		}
		character.Player.CharmingTarget = false;
	}
}
