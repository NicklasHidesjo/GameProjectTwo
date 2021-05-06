using UnityEngine;

[CreateAssetMenu(fileName = "ShouldLeave", menuName = "AI/Decision/ShouldLeave")]
public class ShouldLeave : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.Leave;
	}
}
