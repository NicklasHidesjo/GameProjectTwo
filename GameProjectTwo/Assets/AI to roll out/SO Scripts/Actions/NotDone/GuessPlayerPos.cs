using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "GuessPlayerPos", menuName = "AI/Action/GuessPlayerPos")]
public class GuessPlayerPos : Action
{
	public override void Execute(ICharacter character)
	{
		if(character.TimeSinceLastSeenPlayer <= 0) { return; }

		Vector3 velocity = character.PlayerTransform.GetComponentInParent<CharacterController>().velocity.normalized;
		float distance = Vector3.Distance(character.Transform.position, character.PlayerTransform.position);

		Vector3 guessPos = character.PlayerTransform.position + (velocity * distance);
		guessPos.y = character.PlayerTransform.position.y;

		RaycastHit hit;
		int layerMask = 1 << 9;
		if (Physics.Raycast(character.PlayerTransform.position, velocity, out hit, distance, layerMask))
		{
			if (hit.collider.CompareTag("Building"))
			{
				guessPos = hit.point;
			}
		}

		NavMeshHit estimatedPos;
		if (NavMesh.SamplePosition(guessPos, out estimatedPos, 5f, NavMesh.AllAreas))
		{
			character.PlayerLastSeen = estimatedPos.position;
		}
	}
}
