using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BecomeSuspicious", menuName = "AI/Decision/BecomeSuspicious")]
public class BecomeSuspicious : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.Alertness >= character.Stats.CautiousThreshold;
	}
}
