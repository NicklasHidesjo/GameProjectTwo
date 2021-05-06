using UnityEngine;

[CreateAssetMenu(fileName = "SetAlertnessToHalf", menuName = "AI/Action/SetAlertnessToHalf")]
public class SetAlertnessToHalf : Action
{
	public override void Execute(ICharacter character)
	{
		character.Alertness = character.Stats.MaxAlerted / 2;
	}
}
