using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Flee", menuName = "AI/Action/Flee")]
public class Flee : Action
{
	List<Vector3> fleeAngles = new List<Vector3>();

	public override void Execute(ICharacter character)
	{
		character.FreezeInFear = false;
		if (character.Agent.remainingDistance > character.Agent.stoppingDistance + 0.5f)
		{
			return;
		}
		Vector3 direction = character.Player.position - character.Transform.position;
		if(!character.RayHitPlayer(direction, character.Stats.SightLenght))
		{
			return;
		}

		fleeAngles.Clear();
		Vector3 chaserDir = character.Player.position - character.Transform.position;

		foreach (var angle in character.RunAngles)
		{
			CheckWalkableDirection(chaserDir, angle, character);
		}

		if (fleeAngles.Count < 1)
		{
			character.FreezeInFear = true;
			Debug.Log("No angles left to flee to going into frozeninfear state");
		}
		else
		{
			Vector3 runDir = fleeAngles[Random.Range(0, fleeAngles.Count)];
			GetRunPoint(runDir, character);
		}
	}

	private void CheckWalkableDirection(Vector3 targetDir, Vector3 ownDirection, ICharacter character)
	{
		if (Vector3.Angle(targetDir, ownDirection) >= character.Stats.FleeAngle)
		{
			if (Physics.Raycast(character.Transform.position, ownDirection, character.Stats.ClearanceDistance))
			{
				return;		
			}
			fleeAngles.Add(ownDirection);
		}
	}

	private void GetRunPoint(Vector3 runDir, ICharacter character)
	{
		Vector3 randomPos = (runDir * character.Stats.FleeDistance) + character.Transform.position;

		NavMeshHit hit;
		if (NavMesh.SamplePosition(randomPos, out hit, 5, NavMesh.AllAreas))
		{
			character.Agent.destination = hit.position;
		}
	}
}
