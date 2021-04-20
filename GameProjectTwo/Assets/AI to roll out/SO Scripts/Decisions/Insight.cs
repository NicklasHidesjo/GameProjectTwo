using UnityEngine;

[CreateAssetMenu(fileName = "InSight", menuName = "AI/Decision/InSight")]
public class Insight : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.SeesPlayer;
	}
}
