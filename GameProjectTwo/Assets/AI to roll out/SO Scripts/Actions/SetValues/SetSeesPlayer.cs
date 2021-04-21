using UnityEngine;

[CreateAssetMenu(fileName = "SetSeesPlayer", menuName = "AI/Action/SetSeesPlayer")]
public class SetSeesPlayer : Action
{
	public override void Execute(ICharacter character)
	{
		character.SeesPlayer = false;

		Vector3 direction = character.Player.position - character.Transform.position;
		if (!character.InFrontOff(direction)) { return; }

		character.SeesPlayer = character.RayHitTag("Player", direction, character.Stats.FollowRange);
		if (character.SeesPlayer)
		{
			character.TimeSinceLastSeenPlayer = 0;
		}
	}
}
