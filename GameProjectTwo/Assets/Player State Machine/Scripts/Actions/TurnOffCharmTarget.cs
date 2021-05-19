using UnityEngine;

[CreateAssetMenu(fileName = "TurnOffCharmTarget", menuName = "Player/Action/TurnOffCharmTarget")]
public class TurnOffCharmTarget : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		player.CharmingTarget = false;
		player.charmTarget.IsCharmed = false;
		player.charmTarget = null;
	}
}
