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

	public bool GettingSucked { get; set; }

	public bool IsDead { get;}
	public int CurrentHealth { get;}

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

	/// <summary>
	/// Returns true if we hit the tag within the lenght towards direction
	/// </summary>
	/// <param name="tag"></param>
	/// <param name="lenght"></param>
	/// <returns></returns>
	public bool RayHitTag(string tag, Vector3 direction, float lenght);
	/// <summary>
	/// Returns true if we are infront of the direction
	/// </summary>
	/// <param name="direction"></param>
	/// <returns></returns>
	public bool InFrontOff(Vector3 direction);

	public void DecreaseHealth(int health);
}
