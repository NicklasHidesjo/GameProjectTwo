using UnityEngine;

[CreateAssetMenu(fileName = "IncreasePlayerStateTime", menuName = "Player/Action/IncreasePlayerStateTime")]
public class IncreasePlayerStateTime : PlayerAction
{
	public override void Execute(IPlayer player)
	{
		player.StateTime += Time.deltaTime;
	}
}
