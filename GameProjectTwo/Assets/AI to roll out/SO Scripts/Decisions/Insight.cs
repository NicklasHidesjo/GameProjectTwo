using UnityEngine;

[CreateAssetMenu(fileName = "InSight", menuName = "AI/Decision/InSight")]
public class Insight : Decision
{
	public override bool Decide(ICharacter character)
	{
		Vector3 dir = character.Player.position - character.Transform.position;
		RaycastHit hit;

		if (Physics.Raycast(character.Transform.position, dir, out hit, character.Stats.FollowRange))
		{
			if (hit.collider.gameObject.CompareTag("Player"))
			{
				if (Vector3.Dot(dir, character.Transform.forward) > 1)
				{
					return true;
				}
			}
		}
		return false;
	}
}
