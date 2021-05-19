using UnityEngine;

[CreateAssetMenu(fileName = "EnterCharmedState", menuName = "AI/Decision/EnterCharmedState")]
public class EnterCharmedState : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.IsCharmed;
	}
}
