using UnityEngine;

[CreateAssetMenu(fileName = "PlayerNotInSun", menuName = "Player/Decision/PlayerNotInSun")]
public class PlayerNotInSun : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return !player.InSun;
	}
}
