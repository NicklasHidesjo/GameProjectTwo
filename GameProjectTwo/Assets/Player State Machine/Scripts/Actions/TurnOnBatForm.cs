using UnityEngine;

[CreateAssetMenu(fileName = "TurnOnBatForm", menuName = "Player/Action/TurnOnBatForm")]
public class TurnOnBatForm : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		player.Controller.radius = 0.3f;
		player.Controller.height = 0.5f;
		player.ActivateBatForm();
	}
}
