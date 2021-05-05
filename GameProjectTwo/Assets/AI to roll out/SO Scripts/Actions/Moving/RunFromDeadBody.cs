using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "RunFromDeadBody", menuName = "AI/Action/RunFromDeadBody")]
public class RunFromDeadBody : Action
{
	public override void Execute(ICharacter character)
	{
		if (character.Agent.velocity != Vector3.zero) { return; }
		Vector3 direction = character.DeadNpc.transform.position - character.Transform.position;
		if (character.RayHitPlayer(direction, character.Stats.SightLenght))
		{
			GetRunDestination(character);
		}
	}
	private void GetRunDestination(ICharacter character)
	{
		float distance = Random.Range(10, character.Stats.MaxFleeDistance);

		Vector3 dir = character.Transform.position - character.Player.transform.position;
		float angle = Random.Range(0, 360);

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
