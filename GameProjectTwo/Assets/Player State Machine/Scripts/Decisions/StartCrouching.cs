using UnityEngine;

[CreateAssetMenu(fileName = "StartCrouching", menuName = "Player/Decision/StartCrouching")]
public class StartCrouching : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return Input.GetButtonDown("Crouch");
	}
}
