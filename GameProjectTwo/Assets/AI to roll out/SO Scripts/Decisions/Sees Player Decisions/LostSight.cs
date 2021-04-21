using UnityEngine;

[CreateAssetMenu(fileName = "LostSight", menuName = "AI/Decision/LostSight")]
public class LostSight : Decision
{
	public override bool Decide(ICharacter character)
	{
		return !character.SeesPlayer;
	}
}
