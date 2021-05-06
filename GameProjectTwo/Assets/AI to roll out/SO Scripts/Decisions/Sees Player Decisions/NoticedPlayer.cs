using UnityEngine;

[CreateAssetMenu(fileName = "NoticedPlayer", menuName = "AI/Decision/NoticedPlayer")]
public class NoticedPlayer : Decision
{
	public override bool Decide(ICharacter character)
	{
		return character.NoticedPlayer;
	}
}
