using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "MoveAwayFromPlayer", menuName = "AI/Action/MoveAwayFromPlayer")]
public class MoveAwayFromPlayer : Action
{
	// make bools here that we can tick to have different run behaviours from the same action script.
	public override void Execute(ICharacter character)
	{
		if (character.Agent.velocity == Vector3.zero)
		{
			Vector3 dir = character.Player.position - character.Transform.position;
			Vector3 origin = character.Transform.position;
			RaycastHit hit;

			if (Physics.Raycast(origin, dir, out hit, character.Stats.SightLenght))
			{
				if (hit.collider.CompareTag("Player"))
				{
					GetRunDestination(character);
				}
			}
		}
	}
	private void GetRunDestination(ICharacter character)
	{
		float distance = Random.Range(10, character.Stats.MaxFleeDistance);

		Vector3 dir = character.Transform.position - character.Player.transform.position;
		float angle = Random.Range(0, character.Stats.FleeAngle) * (Random.Range(0, 1) * 2 - 1);

		Vector3 randomDir = Quaternion.AngleAxis(angle, dir) * dir;
		randomDir.Normalize();

		Vector3 randomPos = (randomDir * distance) + character.Transform.position;

		NavMeshHit hit;
		NavMesh.SamplePosition(randomPos, out hit, distance, 1);

		character.Agent.destination = hit.position;

		Debug.DrawRay(character.Transform.position, randomDir, Color.black, 5f); // remove this once testing is done
	}
}
