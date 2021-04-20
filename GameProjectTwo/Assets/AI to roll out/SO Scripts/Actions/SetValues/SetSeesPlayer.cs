using UnityEngine;

[CreateAssetMenu(fileName = "SetSeesPlayer", menuName = "AI/Action/SetSeesPlayer")]
public class SetSeesPlayer : Action
{
	public override void Execute(ICharacter character)
	{
		character.SeesPlayer = false;

		Vector3 dir = character.Player.position - character.Transform.position;
		Vector3 origin = character.Transform.position;
		RaycastHit hit;

		if (Physics.Raycast(origin, dir, out hit, character.Stats.FollowRange))
		{
			if (!hit.collider.gameObject.CompareTag("Player")) { return; }
			if (!(Vector3.Dot(dir, character.Transform.forward) > 1)) { return; }
			character.SeesPlayer = true;
			character.TimeSinceLastSeenPlayer = 0;
		}
	}
}
