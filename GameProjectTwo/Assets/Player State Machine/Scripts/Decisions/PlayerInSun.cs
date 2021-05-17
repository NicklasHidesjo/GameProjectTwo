using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInSun", menuName = "Player/Decision/PlayerInSun")]
public class PlayerInSun : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return player.InSun;
	}
}
