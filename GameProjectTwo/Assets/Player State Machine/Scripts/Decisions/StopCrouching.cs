using UnityEngine;

[CreateAssetMenu(fileName = "StopCrouching", menuName = "Player/Decision/StopCrouching")]
public class StopCrouching : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return Input.GetButtonUp("Crouch");
	}
}
