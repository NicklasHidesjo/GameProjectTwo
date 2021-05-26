using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToMovingIdle", menuName = "AI/Decision/ToMovingIdle")]
public class ToMovingIdle : Decision
{
	public override bool Decide(ICharacter character)
	{
		if(character.Stationary)
		{
			return false;
		}
		return character.Alertness <= character.Stats.CautiousThreshold;
	}
}

