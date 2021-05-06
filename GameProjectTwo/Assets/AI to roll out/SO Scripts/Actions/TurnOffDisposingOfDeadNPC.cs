using UnityEngine;

[CreateAssetMenu(fileName = "TurnOffDisposingOfDeadNPC", menuName = "AI/Action/TurnOffDisposingOfDeadNPC")]
public class TurnOffDisposingOfDeadNPC : Action
{
	public override void Execute(ICharacter character)
	{
        if(character.DeadNpc == null)
		{
			return;
		}
		character.DeadNpc.GettingDisposed = false;
	}
}
