using UnityEngine;

[CreateAssetMenu(fileName = "StopSearching", menuName = "AI/Decision/StopSearching")]
public class StopSearching : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.Alertness < character.Stats.CautiousThreshold;
	}
}
