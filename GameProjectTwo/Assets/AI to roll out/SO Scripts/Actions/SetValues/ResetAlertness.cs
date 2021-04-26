using UnityEngine;

[CreateAssetMenu(fileName = "ResetAlertness", menuName = "AI/Action/ResetAlertness")]
public class ResetAlertness : Action
{
	public override void Execute(ICharacter character)
	{
		Vector3 direction = character.Player.position - character.Transform.position;
		if (!character.RayHitPlayer(direction, character.Stats.SightLenght))
		{
			character.LowerAlertness(character.Stats.MaxAlerted +1); // the plus one is to make sure we go to 0
		}
	}
}
