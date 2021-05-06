using UnityEngine;

[CreateAssetMenu(fileName = "ExitingCharmed", menuName = "AI/Action/ExitingCharmed")]
public class ExitingCharmed : Action
{
	public override void Execute(ICharacter character)
	{
        if(character.SeesPlayer)
		{
			character.SetAlertnessToMax();
		}
		else
		{
			character.SetAlertness(character.Stats.ExitCharmIncrease);
		}
	}
}
