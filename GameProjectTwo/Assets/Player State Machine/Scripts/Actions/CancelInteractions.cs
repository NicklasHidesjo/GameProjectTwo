using UnityEngine;

[CreateAssetMenu(fileName = "CancelInteractions", menuName = "Player/Action/CancelInteractions")]
public class CancelInteractions : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		player.PlayerObjectInteract.CancelInteraction();
	}
}
