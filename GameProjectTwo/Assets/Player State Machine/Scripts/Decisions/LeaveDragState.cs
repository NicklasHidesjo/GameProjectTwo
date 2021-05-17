using UnityEngine;

[CreateAssetMenu(fileName = "LeaveDragState", menuName = "Player/Decision/LeaveDragState")]
public class LeaveDragState : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return !player.DraggingBody;
	}
}
