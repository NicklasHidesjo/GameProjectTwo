using UnityEngine;

[CreateAssetMenu(fileName = "SeesPlayer", menuName = "AI/Decision/SeesPlayer")]
public class SeesPlayer : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.SeesPlayer;
	}
}
