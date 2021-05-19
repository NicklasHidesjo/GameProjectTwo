using UnityEngine;

[CreateAssetMenu(fileName = "TurnOffCharmRenderer", menuName = "AI/Action/TurnOffCharmRenderer")]
public class TurnOffCharmRenderer : Action
{
	public override void Execute(ICharacter character)
	{
		character.SetCharmInteraction(false);
	}
}
