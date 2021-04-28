using UnityEngine;

[CreateAssetMenu(fileName = "EnterFreezeInFear", menuName = "AI/Decision/EnterFreezeInFear")]
public class EnterFreezeInFear : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.FreezeInFear;
	}
}
