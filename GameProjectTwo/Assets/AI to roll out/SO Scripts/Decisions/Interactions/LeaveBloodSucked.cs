using UnityEngine;

[CreateAssetMenu(fileName = "LeaveBloodSucked", menuName = "AI/Decision/LeaveBloodSucked")]
public class LeaveBloodSucked : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.IsDead;
	}
}
