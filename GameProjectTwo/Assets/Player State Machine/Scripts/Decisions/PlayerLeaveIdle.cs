using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLeaveIdle", menuName = "Player/Decision/PlayerLeaveIdle")]
public class PlayerLeaveIdle : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return player.Controller.velocity != Vector3.zero;
	}
}
