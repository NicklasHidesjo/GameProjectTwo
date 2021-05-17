using UnityEngine;

[CreateAssetMenu(fileName = "EnterDragState", menuName = "Player/Decision/EnterDragState")]
public class EnterDragState : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return player.DraggingBody;
	}
}
