using UnityEngine;

[CreateAssetMenu(fileName = "Interact", menuName = "Player/Action/Interact")]
public class Interact : PlayerAction
{
	public override void Execute(IPlayer player)
	{
        if (Input.GetButtonDown("Interact"))
        {
            if (player.IScanner.CurrentInteractable == null)
            {
                return;
            }
            player.Interactable = player.IScanner.CurrentInteractable;
            player.PlayerObjectInteract.InteractWithObject(player.Interactable);
        }
    }
}
