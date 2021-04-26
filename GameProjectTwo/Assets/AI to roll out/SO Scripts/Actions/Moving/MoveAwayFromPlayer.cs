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
		if(character.Agent.velocity != Vector3.zero) { return; }
		Vector3 direction = character.Player.position - character.Transform.position;
		if (character.RayHitPlayer(direction, character.Stats.SightLenght))
		{
			GetRunDestination(character);
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
		if(NavMesh.SamplePosition(randomPos, out hit, 5,NavMesh.AllAreas))
		{
			character.Move(hit.position);
		}
	}
}
