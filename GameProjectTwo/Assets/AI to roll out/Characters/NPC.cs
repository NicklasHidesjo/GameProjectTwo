using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

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
	public bool GettingSucked { get; set; }
	public bool IsSuckable { get; set; }

	private bool isDead;
	public bool IsDead => isDead;
	[SerializeField] private int currentHealth;
	public int CurrentHealth => currentHealth;

	public bool ShouldShout { get; set; }

	private List<NPC> nearbyCharacters = new List<NPC>();
	public List<NPC> NearbyCharacters => nearbyCharacters;

	public bool Run { get; set; }

	public bool NoticedPlayer { get; set; }

	private void Awake()
	{
		if (stats == null)
		{
			Debug.LogError("This npc doesnt have any stats: " + gameObject.name);
			//UnityEditor.EditorApplication.isPlaying = false;
		}
		InitializeNPC();
	}
	private void InitializeNPC()
	{
		agent = GetComponent<NavMeshAgent>();
		player = FindObjectOfType<PlayerManager>().GetPlayerPoint();
		path = GameObject.FindGameObjectsWithTag(pathTag).Select(f => f.transform).ToArray();
		IsSuckable = true;
		isDead = false;
		ShouldShout = true;
		currentHealth = stats.MaxHealth;
		agent.speed = stats.WalkSpeed;
		Alertness = 0;
		StateTime = 0;
		PathIndex = 0;
		YRotCorrection = 0;
		SearchAngle = stats.SearchAngle;
		RotationSpeed = stats.RotationSpeed;
		// set the spherecollider radius here using a stat in npc stats?
	}


	private void FixedUpdate()
	{
		Debug.Log(StateTime);
		SetNearbyCharacters();
	}

	private void SetNearbyCharacters()
	{
		nearbyCharacters.Clear();
		int layerMask = 1 << 7;
		Collider[] npcClose = Physics.OverlapSphere(transform.position, stats.ShoutRange, layerMask);
		foreach (var character in npcClose)
		{
			if (character.GetComponent<NPC>() == this) { continue; }
			nearbyCharacters.Add(character.GetComponent<NPC>());
		}
	}

	public void Attack()
	{
		Debug.Log("i am now attacking the player. I deal this much damage: " + stats.Damage);
		PlayerManager.instance.gameObject.GetComponent<PlayerStatsManager>().DecreaseHealthValue(stats.Damage);
	}
	public void Move(Vector3 destination)
	{
		agent.destination = destination;
	}
	public void LookAt(Vector3 Target)
	{
		transform.LookAt(Target);
	}
	public bool RayHitPlayer(Vector3 direction, float lenght)
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, direction, out hit, lenght))
		{
			if (hit.collider.CompareTag("Player"))
			{
				return true;
			}
		}
		return false;
	}
	public bool InFrontOff(Vector3 direction)
	{
		Vector3 dirToTarget = (player.transform.position - transform.position).normalized;
		return Vector3.Angle(transform.forward, dirToTarget) < stats.FieldOfView / 2;
	}

	public void DecreaseHealth(int health)
	{
		currentHealth -= health;
		if(currentHealth <= 0)
		{
			isDead = true;
		}	
	}

	public void ReactToShout()
	{
		if (gameObject.CompareTag("Civilian"))
		{
			if (Vector3.Distance(transform.position, player.position) > stats.SightLenght) { return; } // have this be a check if we se the player instead
		}
		ShouldShout = false;

		SetAlertnessToMax();
	}

	public void SetAlertnessToMax()
	{
		Alertness = stats.MaxAlerted;
		Run = true;
	}

	public void RaiseAlertness(bool inFOW)
	{
		float value = stats.AlertIncrease * Time.deltaTime;
		if(inFOW)
		{
			value *= stats.InSightMultiplier;
		}
		Alertness = Mathf.Clamp(Alertness + Mathf.Abs(value), 0, stats.MaxAlerted);
		if (Alertness >= stats.MaxAlerted)
		{
			Run = true;
		}
	}
	public void LowerAlertness(float value)
	{
		Alertness = Mathf.Clamp(Alertness - Mathf.Abs(value), 0, stats.MaxAlerted);
	}
}
