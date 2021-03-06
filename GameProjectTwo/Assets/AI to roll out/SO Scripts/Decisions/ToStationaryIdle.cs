using UnityEngine;

[CreateAssetMenu(fileName = "ToStationaryIdle", menuName = "AI/Decision/ToStationaryIdle")]
public class ToStationaryIdle : Decision
{
	public override bool Decide(ICharacter character)
	{
		if (!character.Stationary)
		{
			return false;
		}
		return character.Alertness <= character.Stats.CautiousThreshold;
	}
}
