using UnityEngine;

[CreateAssetMenu(fileName = "TurnOnDraculaForm", menuName = "Player/Action/TurnOnDraculaForm")]
public class TurnOnDraculaForm : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		player.Controller.radius = 0.5f;
		player.Controller.height = 2f;
		player.ActivateDraculaForm();
	}
}
