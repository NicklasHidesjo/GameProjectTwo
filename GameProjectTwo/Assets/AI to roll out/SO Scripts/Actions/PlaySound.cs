using UnityEngine;

[CreateAssetMenu(fileName = "PlaySound", menuName = "AI/Action/PlaySound")]
public class PlaySound : Action
{
	[SerializeField] SoundType audio;
	public override void Execute(ICharacter character)
	{
		AudioManager.instance.PlaySound(audio, character.Self.gameObject);
	}
}
