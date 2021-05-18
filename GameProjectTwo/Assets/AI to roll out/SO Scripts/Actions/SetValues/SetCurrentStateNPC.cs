using UnityEngine;

[CreateAssetMenu(fileName = "SetCurrentStateNPC", menuName = "AI/Action/SetCurrentStateNPC")]
public class SetCurrentStateNPC : Action
{
	[SerializeField] NPCStates state;
	public override void Execute(ICharacter character)
	{
		character.CurrentState = state;
	}
}
