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

	public float RotationTime { get; set; }
	
	public Vector3 PlayerLastSeen { get; set; }

	public bool SeesPlayer { get; set; }

	public Quaternion OriginRot { get; set; }
	public Quaternion TargetRot { get; set; }
	public bool InfrontOfWall { get; set; }
	public float SearchAngle { get; set; }
	public float YRotCorrection { get; set; }
	public float RotationSpeed { get; set; }
	public bool RotationStarted { get; set; }

	public void Move(Vector3 destination);
	public void Attack();
	public void LookAt(Vector3 Target);
}
