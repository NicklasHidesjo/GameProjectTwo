using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StartRunning", menuName = "AI/Decision/StartRunning")]
public class StartRunning : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.Alertness >= character.Stats.MaxAlerted - 0.1f;
	}
}
