using UnityEngine;

[CreateAssetMenu(fileName = "ResetContainerInteractionDone", menuName = "Player/Action/ResetContainerInteractionDone")]
public class ResetContainerInteractionDone : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		player.ContainerInteractionDone = false;
	}
}
