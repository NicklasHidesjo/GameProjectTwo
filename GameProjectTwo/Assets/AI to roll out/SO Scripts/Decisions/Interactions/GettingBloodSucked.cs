using UnityEngine;

[CreateAssetMenu(fileName = "GettingBloodSucked", menuName = "AI/Decision/GettingBloodSucked")]
public class GettingBloodSucked : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.GettingSucked;
	}
}
