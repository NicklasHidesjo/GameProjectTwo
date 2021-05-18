using UnityEngine;

[CreateAssetMenu(fileName = "StopInteracting", menuName = "Player/Decision/StopInteracting")]
public class StopInteracting : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return Input.GetButtonDown("Run") || Input.GetButtonDown("Interact");
	}
}
