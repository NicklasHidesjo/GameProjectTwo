using UnityEngine;

[CreateAssetMenu(fileName = "TrySetLastKnown", menuName = "AI/Action/TrySetLastKnown")]
public class TrySetLastKnown : Action
{
	public override void Execute(ICharacter character)
	{
		if (!character.SeesPlayer)
		{
			return;
		}
		character.PlayerLastSeen = character.PlayerTransform.position;
	}
}
