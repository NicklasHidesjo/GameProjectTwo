using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResetAgentPath", menuName = "AI/Action/ResetAgentPath")]
public class ResetAgentPath : Action
{
	public override void Execute(ICharacter character)
	{
		character.Move(character.Transform.position);
	}
}
