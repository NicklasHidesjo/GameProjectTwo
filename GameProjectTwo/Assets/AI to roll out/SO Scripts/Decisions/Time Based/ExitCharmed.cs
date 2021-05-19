using UnityEngine;

[CreateAssetMenu(fileName = "ExitCharmed", menuName = "AI/Decision/ExitCharmed")]
public class ExitCharmed : Decision
{
	public override bool Decide(ICharacter character)
	{
		return !character.IsCharmed;
	}
}
