using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "RunFromDeadBody", menuName = "AI/Action/RunFromDeadBody")]
public class RunFromDeadBody : Action
{
	public override void Execute(ICharacter character)
	{
		if (character.Agent.velocity != Vector3.zero) { return; }
		Vector3 direction = character.DeadNpc.transform.position - character.Transform.position;
		if (character.RayHitDeadNPC(direction, character.Stats.SightLenght))
		{
			GetRunDestination(character);
		}
	}
	private void GetRunDestination(ICharacter character)
	{
		float distance = Random.Range(10, character.Stats.MaxFleeDistance);

		Vector3 dir = character.DeadNpc.transform.position - character.Transform.position;
		float angle = Random.Range(0, character.Stats.FleeDeadAngle) * (Random.Range(0, 2) * 2 - 1);

		Vector3 randomDir = Quaternion.AngleAxis(angle, character.Transform.up) * dir;
		randomDir.Normalize();

		Vector3 randomPos = character.Transform.position + (-randomDir * distance);

		NavMeshHit hit;
		if (NavMesh.SamplePosition(randomPos, out hit, 5, NavMesh.AllAreas))
		{
			character.Move(hit.position);
		}
	}
}
