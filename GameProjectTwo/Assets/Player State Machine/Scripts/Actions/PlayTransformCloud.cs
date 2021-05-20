using UnityEngine;

[CreateAssetMenu(fileName = "PlayTransformCloud", menuName = "Player/Action/PlayTransformCloud")]
public class PlayTransformCloud : PlayerAction
{
	[SerializeField] GameObject smokePuff;
	[SerializeField] float destroyDelay = 5f;
	[SerializeField] bool TransformToBat;
	public override void Execute(IPlayer player)
	{
		GameObject puff = Instantiate(smokePuff, player.Transform.position, Quaternion.identity);
		Destroy(puff, destroyDelay);
        if (TransformToBat)
        {
			AudioManager.instance.PlaySound(SoundType.BatTransform, player.Transform.gameObject);
        }
        else
        {
			AudioManager.instance.PlaySound(SoundType.DraculaTransform, player.Transform.gameObject);

		}
	}
}
