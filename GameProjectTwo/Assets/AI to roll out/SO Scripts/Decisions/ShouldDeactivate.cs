using UnityEngine;

[CreateAssetMenu(fileName = "ShouldDeactivate", menuName = "AI/Decision/ShouldDeactivate")]
public class ShouldDeactivate : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.StateTime > character.Stats.FadeDuration;
	}
}
