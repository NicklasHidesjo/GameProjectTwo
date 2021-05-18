using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "StartFleeing", menuName = "AI/Action/StartFleeing")]
public class StartFleeing : Action
{
	public override void Execute(ICharacter character)
	{
		Vector3 direction = character.PlayerTransform.position - character.Transform.position;
		if (character.RayHitPlayer(direction, character.Stats.SightLenght))
		{
			GetRunDestination(character);
		}
	}
	private void GetRunDestination(ICharacter character)
	{
		float distance = Random.Range(10, character.Stats.MaxFleeDistance);

		Vector3 dir = character.Transform.position - character.PlayerTransform.transform.position;
		float angle = Random.Range(0, character.Stats.FleeDeadAngle) * (Random.Range(0, 1) * 2 - 1);

		Vector3 randomDir = Quaternion.AngleAxis(angle, dir) * dir;
		randomDir.Normalize();

		Vector3 randomPos = (randomDir * distance) + character.Transform.position;

		NavMeshHit hit;
		if (NavMesh.SamplePosition(randomPos, out hit, 5, NavMesh.AllAreas))
		{
			character.Move(hit.position);
		}
	}
}

