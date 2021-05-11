using UnityEngine;

[CreateAssetMenu(fileName = "LeaveBloodSucked", menuName = "AI/Decision/LeaveBloodSucked")]
public class LeaveBloodSucked : Decision
{
	public override bool Decide(ICharacter character)
	{
		// might be able to remove the check if the character is getting sucked
		return !character.GettingSucked && character.StateTime >= character.Stats.SuckedStun;
	}
}
