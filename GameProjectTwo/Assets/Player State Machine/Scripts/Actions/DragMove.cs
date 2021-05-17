using UnityEngine;

[CreateAssetMenu(fileName = "DragMove", menuName = "Player/Action/DragMove")]
public class DragMove : PlayerAction
{
	public override void Execute(IPlayer player)
	{
        if(player.StateTime < player.Stats.InteractionTime)
		{
			return;
		}
		if(!player.DoneRotating)
		{
			return;
		}

	}
}
