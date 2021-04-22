using UnityEngine;

[CreateAssetMenu(fileName = "ResetAlertness", menuName = "AI/Action/ResetAlertness")]
public class ResetAlertness : Action
{
	public override void Execute(ICharacter character)
	{
		Vector3 direction = character.Player.position - character.Transform.position;
		if (!character.RayHitTag("Player", direction, character.Stats.SightLenght))
		{
			character.Alertness = 0;
		}
	}
}
