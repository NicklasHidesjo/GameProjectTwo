using UnityEngine;

[CreateAssetMenu(fileName = "ContainerInteractionDone", menuName = "Player/Decision/ContainerInteractionDone")]
public class ContainerInteractionDone : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return player.ContainerInteractionDone;
	}
}
