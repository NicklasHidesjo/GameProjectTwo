using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "SearchAround", menuName = "AI/Action/SearchAround")]
public class SearchAround : Action
{
	[SerializeField] GameObject spawnObject;
	public override void Execute(ICharacter character)
	{
		if(character.Agent.remainingDistance >= character.Agent.stoppingDistance)
		{
			return;
		}
		if(character.Agent.velocity != Vector3.zero)
		{
			return;
		}


		float distance = Random.Range(5, character.Stats.SearchRadius);

		Vector3 randomDir = Random.insideUnitSphere;

		Vector3 randomPos = character.Transform.position;

		RaycastHit hit;

		if (Physics.Raycast(character.PlayerLastSeen, randomDir, out hit, distance))
		{
			if (hit.collider != null)
			{
				randomPos = hit.point;
			}
		}

		NavMeshHit navHit;
		NavMesh.SamplePosition(randomPos, out navHit, distance, 1);

		Instantiate(spawnObject, randomPos, Quaternion.identity, character.Transform.parent);
		Instantiate(spawnObject, character.PlayerLastSeen, Quaternion.identity, character.Transform.parent);

		character.Move(navHit.position);
	}
}
