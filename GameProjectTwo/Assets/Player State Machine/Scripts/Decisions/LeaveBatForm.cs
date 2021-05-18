using UnityEngine;

[CreateAssetMenu(fileName = "LeaveBatForm", menuName = "Player/Decision/LeaveBatForm")]
public class LeaveBatForm : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return player.CurrentStamina <= 0 || player.LeaveBat;
	}
}
