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
            Interactable interactable = player.IScanner.CurrentInteractable;
            player.PlayerObjectInteract.InteractWithObject(interactable);
        }
    }
}
