using UnityEngine;

[CreateAssetMenu(fileName = "PlayTransformCloud", menuName = "Player/Action/PlayTransformCloud")]
public class PlayTransformCloud : PlayerAction
{
	[SerializeField] GameObject smokePuff;
	[SerializeField] float destroyDelay = 5f;
	public override void Execute(IPlayer player)
	{
		GameObject puff = Instantiate(smokePuff, player.Transform.position, Quaternion.identity);
		Destroy(puff, destroyDelay);
	}
}
