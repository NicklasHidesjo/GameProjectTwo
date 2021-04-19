using UnityEngine;
using UnityEngine.AI;
public interface ICharacter
{
	public float Alertness { get; set; }
	public int PathIndex { get; set; }
	public float StateTime { get; set; }
	public Transform[] Path { get; }
	public Transform Transform { get; }
	public Transform Player { get; }
	public NavMeshAgent Agent { get; }
	public NPCStats Stats { get; }
	public float TimeSinceLastAction { get; set; }
	public float TimeSinceLastSeenPlayer { get; set; }
	public Vector3 LastKnown { get; set; }

	public bool LookRight { get; set; }

	public void Move(Vector3 destination);
	public void Attack();
	public void LookAt(Vector3 Target);
}
