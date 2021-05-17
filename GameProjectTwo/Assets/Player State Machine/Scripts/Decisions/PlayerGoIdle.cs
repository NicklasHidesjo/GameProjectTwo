using UnityEngine;

[CreateAssetMenu(fileName = "PlayerGoIdle", menuName = "Player/Decision/PlayerGoIdle")]
public class PlayerGoIdle : PlayerDecision
{
	public override bool Decide(IPlayer player)
	{
		return player.Controller.velocity == Vector3.zero;
	}
}
