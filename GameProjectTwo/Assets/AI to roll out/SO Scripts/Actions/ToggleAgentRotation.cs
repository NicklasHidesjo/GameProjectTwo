using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToggleAgentRotation", menuName = "AI/Action/ToggleAgentRotation")]
public class ToggleAgentRotation : Action
{
	[SerializeField] bool updateRotation;
	public override void Execute(ICharacter character)
	{
		character.Agent.updateRotation = updateRotation;
	}
}
