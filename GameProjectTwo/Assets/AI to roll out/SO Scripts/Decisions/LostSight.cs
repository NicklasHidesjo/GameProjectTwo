using UnityEngine;

[CreateAssetMenu(fileName = "LostSight", menuName = "AI/Decision/LostSight")]
public class LostSight : Decision
{
	public override bool Decide(ICharacter character)
	{
		Vector3 dir = character.Player.position - character.Transform.position;
		Vector3 origin = character.Transform.position;
		RaycastHit hit;

		if (Physics.Raycast(origin, dir, out hit, character.Stats.FollowRange))
		{
			if (hit.collider.gameObject.CompareTag("Player"))
			{
				return false;
			}
		}
		return true;
	}
}
