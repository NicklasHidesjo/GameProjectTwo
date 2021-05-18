using UnityEngine;

[CreateAssetMenu(fileName = "ResetPlayerStateTime", menuName = "Player/Action/ResetPlayerStateTime")]
public class ResetPlayerStateTime : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		player.StateTime = 0;
	}
}
