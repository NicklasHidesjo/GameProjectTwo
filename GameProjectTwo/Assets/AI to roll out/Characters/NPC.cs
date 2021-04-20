using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent), typeof(StateMachine))]
public class NPC : MonoBehaviour, ICharacter
{
	[SerializeField] NPCStats stats;
	[Tooltip("The tag that will be used to find all path points when walking (ex POI or Waypoint")]
	[SerializeField] string pathTag;

	private NavMeshAgent agent;
	private Transform player;
	private Transform[] path;

	public Transform[] Path => path;

	public Transform Transform => transform;

	public Transform Player => player;

	public NavMeshAgent Agent => agent;

	public NPCStats Stats => stats;

	public float StateTime { get; set; }

	public float TimeSinceLastSeenPlayer { get; set; }
	public float TimeSinceLastAction { get; set; }
	public float Alertness { get; set; }
	public int PathIndex { get; set; }

	public Vector3 PlayerLastSeen { get; set; }
	public bool SeesPlayer { get; set; }

	public Quaternion OriginRot { get; set; }
	public Quaternion TargetRot { get; set; }
	public bool InfrontOfWall { get; set; }
	public float RotationTime { get; set; }
	public float SearchAngle { get; set; }
	public float YRotCorrection { get; set; }
	public float RotationSpeed { get; set; }
	public bool RotationStarted { get; set ; }

	private void Awake()
	{
		if (stats == null)
		{
			Debug.LogError("This npc doesnt have any stats: " + gameObject.name);
			UnityEditor.EditorApplication.isPlaying = false;
		}
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		path = GameObject.FindGameObjectsWithTag(pathTag).Select(f => f.transform).ToArray();
		Alertness = 0;
		StateTime = 0;
		PathIndex = 0;
		YRotCorrection = 0;
		SearchAngle = stats.SearchAngle;
		RotationSpeed = stats.RotationSpeed;
		// set the spherecollider radius here using a stat in npc stats?
	}

	public void Attack()
	{
		Debug.Log("i am now attacking the player");
		// add the correct get on the player transform here and do the actual attack.
	}

	public void Move(Vector3 destination)
	{
		agent.destination = destination;
	}

	public void LookAt(Vector3 Target)
	{
		transform.LookAt(Target);
	}
}
