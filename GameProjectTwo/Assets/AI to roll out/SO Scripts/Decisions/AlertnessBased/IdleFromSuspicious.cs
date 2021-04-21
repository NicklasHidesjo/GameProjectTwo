using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleFromSuspicious", menuName = "AI/Decision/IdleFromSuspicious")]
public class IdleFromSuspicious : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.Alertness <= character.Stats.CautiousThreshold;
	}
}

