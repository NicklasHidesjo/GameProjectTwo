using UnityEngine;

[CreateAssetMenu(fileName = "ResetCharmTarget", menuName = "Player/Action/ResetCharmTarget")]
public class ResetCharmTarget : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		if (player.charmTarget != null)
		{
			player.charmTarget.SetCharmInteraction(false);
			player.charmTarget = null;
		}
	}
}
