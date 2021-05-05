using UnityEngine;

[CreateAssetMenu(fileName = "HaveDeadNPCToHandle", menuName = "AI/Decision/HaveDeadNPCToHandle")]
public class HaveDeadNPCToHandle : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.DeadNpc != null;
	}
}
