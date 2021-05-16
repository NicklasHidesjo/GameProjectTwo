using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public interface ICharacter
{
	public float Alertness { get; set; }	
	public int PathIndex { get; set; }	
	public float StateTime { get; set; }
	public PathPoint[] Path { get; }	
	public PathPoint targetPoint { get; set; }
	public Transform Transform { get; }
	public Transform Player { get; }
	public NavMeshAgent Agent { get; }
	public NPCStats Stats { get; }
	public NPC Self { get; }
	public float TimeSinceLastAction { get; set; }
	public float TimeSinceLastSeenPlayer { get; set; }
	public float RotationTime { get; set; }	
	public Vector3 PlayerLastSeen { get; set; }
	public bool SeesPlayer { get; set; }
	public Quaternion OriginRot { get; set; }
	public Quaternion TargetRot { get; set; }
	public bool InfrontOfWall { get; set; }
	
	public float SearchAngle { get; set; }

	public Vector3[] RunAngles { get; set; }

	public float YRotCorrection { get; set; }
	public float RotationSpeed { get; set; }
	public bool RotationStarted { get; set; }

	public bool GettingSucked { get; set; }

	public bool IsDead { get;}
	public int CurrentHealth { get;}

	public bool IsSuckable { get; set; }
	public bool ShouldShout { get; set; }
	public bool Run { get; set; }
	public bool NoticedPlayer { get; set; }
	public bool IsCharmed { get; set; }

	public bool FreezeInFear { get; set; }

	public int FOV { get; set; }

	public NPC DeadNpc { get; set; }

	public Vector3 StartingPosition { get; set; }
	public Quaternion StartingRotation { get; set; }
	public bool Stationary { get; }

	public bool BackTrack { get; set; }
	public bool Increase { get; set; }

	public bool Leave { get; set; }


	public bool GettingDisposed { get; set; }
	public bool Disposed { get; set; }

	public LayerMask NpcLayer { get; }


	public SpawnPath startingPath { get; set; }

	public void InitializeNPC(PathPoint[] path = null, bool backTrack = false);


	/// <summary>
	/// Sets the NavMesh Agents destination to "destination"
	/// </summary>
	/// <param name="destination"></param>
	public void Move(Vector3 destination);

	public void Attack();
	/// <summary>
	/// Rotates the gameObject this script is attached on towards the target
	/// </summary>
	/// <param name="target"></param>
	public void LookAt(Vector3 target);
	public void LookAt(Quaternion target);
	public void RotateTowardsPlayer();

	/// <summary>
	/// Returns true if we hit the player within the length towards direction
	/// </summary>
	/// <param name="tag"></param>
	/// <param name="lenght"></param>
	/// <returns></returns>
	public bool RayHitPlayer(Vector3 direction, float lenght);
	/// <summary>
	/// Returns true if we hit the currently saved deadNPC reference on our npc
	/// </summary>
	/// <param name="direction"></param>
	/// <param name="length"></param>
	/// <returns></returns>
	public bool RayHitDeadNPC(Vector3 direction, float length);

	public void ReactToShout();

	public void DecreaseHealth(int health);

	public void RaiseAlertness(bool inFOW);

	public void LowerAlertness(float value);

	public void SetAlertnessToMax();
	public void SetAlertness(float value);

	public void ReactToShout(NPC deadNPC);

	public void Dispose();
}
