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
	[Tooltip("Set this to true if we want the npc to be stationary")]
	[SerializeField] bool stationary;
	[Tooltip("The layer that npc's are at (used when trying to hit player and also when finding other npc's")]
	[SerializeField] LayerMask npcLayer;

	private NavMeshAgent agent;
	private Transform player;
	private List<Transform> path;

	public LayerMask NpcLayer => npcLayer;

	public NPC Self => this;
	public List<Transform> Path => path;

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

	public Vector3[] RunAngles { get; set; }
	public bool Run { get; set; }

	public bool NoticedPlayer { get; set; }
	public bool SeesPlayer { get; set; }

	public Vector3 StartingPosition { get; set; }
	public Quaternion StartingRotation { get; set; }

	public bool FreezeInFear { get; set; }

	public int FOV { get; set; }

	public bool Stationary => stationary;

	public bool BackTrack { get; set; }
	public bool Increase { get; set; }

	public bool SawHiding { get; set; }

	public bool SawTransforming { get; set; }

	public bool IsCharmed { get; set; }

	public NPC DeadNpc { get; set; }
	public bool GettingDisposed { get; set; }
	public bool Disposed { get; set; }

	Transform[] bodyParts;
	public Transform[] BodyParts => bodyParts;

	public bool Leave { get; set; }

	public bool WalkRandomly { get; set; }

	BloodSuckTarget bloodSuckTarget;

	private void Awake()
	{
		if (stats == null)
		{
			Debug.LogError("This npc doesnt have any stats: " + gameObject.name);
		}
		GetComponents();
	}
	public void GetComponents()
	{
		agent = GetComponent<NavMeshAgent>();
		player = FindObjectOfType<PlayerManager>().GetPlayerPoint();
		bloodSuckTarget = GetComponent<BloodSuckTarget>();
	}

	public void InitializeNPC()
	{
		Destroy(GetComponent<DeadBody>());

		if(gameObject.CompareTag("Civilian"))
		{
			if(bloodSuckTarget == null)
			{
				gameObject.AddComponent<BloodSuckTarget>();
			}
			bloodSuckTarget = GetComponent<BloodSuckTarget>();
		}

		agent.enabled = true;
		path = new List<Transform>();
		path = GameObject.FindGameObjectsWithTag(pathTag).Select(f => f.transform).ToList();
		PathIndex = Random.Range(0, path.Count);
		if(stationary)
		{
			StartingPosition = transform.position;
			StartingRotation = transform.rotation;
		}
		DeadNpc = null;
		SetBools();
		SetFloatsAndInts();
		SetArrays();
		GetComponent<StateMachine>().InitializeStateMachine();
	}

	private void SetBools()
	{
		IsSuckable = true;
		isDead = false;
		ShouldShout = true;
		GettingDisposed = false;
		Disposed = false;
		Leave = false;
		if(gameObject.CompareTag("Guard"))
		{
			WalkRandomly = false;
			Increase = Random.Range(0, 2) * 2 - 1 > 0;
			BackTrack = Random.Range(0, 2) * 2 - 1 > 0;
		}
		else
		{
			WalkRandomly = Random.Range(0, 2) * 2 - 1 > 0;
			Increase = Random.Range(0, 2) * 2 - 1 > 0;
			BackTrack = false;
		}
	}
	private void SetFloatsAndInts()
	{
		currentHealth = stats.MaxHealth;
		agent.speed = stats.WalkSpeed;
		Alertness = 0;
		StateTime = 0;
		PathIndex = 0;
		YRotCorrection = 0;
		SearchAngle = stats.SearchAngle;
		RotationSpeed = stats.SearchRotationSpeed;
		FOV = stats.RelaxedFOV;
	}
	private void SetArrays()
	{
		bodyParts = GetComponentsInChildren<Transform>().Where(t => t.CompareTag("NpcPart")).ToArray();

		RunAngles = new Vector3[8];
		RunAngles[0] = transform.forward;
		RunAngles[1] = transform.forward + transform.right;
		RunAngles[2] = transform.right;
		RunAngles[3] = transform.right - transform.forward;
		RunAngles[4] = -transform.forward;
		RunAngles[5] = -transform.forward - transform.right;
		RunAngles[6] = -transform.right;
		RunAngles[7] = -transform.right + transform.forward;
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

	public void RotateTowardsPlayer()
	{
		Quaternion target = Quaternion.LookRotation(player.position - transform.position);
		
		float compensation = stats.TurnSpeedCompensation - Vector3.Angle(target.eulerAngles, transform.forward);
		float speedIncrease = Mathf.Clamp(compensation, 0, stats.TurnSpeedCompensation);
		float speed = (stats.TurnSpeed + speedIncrease) * Time.fixedDeltaTime;

		Quaternion rotation = Quaternion.Slerp(transform.rotation, target, speed);
		transform.rotation = rotation;
	}
	public void LookAt(Quaternion Target)
	{
		transform.rotation = Target;
	}

	public bool RayHitPlayer(Vector3 direction, float length)
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, direction, out hit, length, ~npcLayer))
		{
			if (hit.collider.CompareTag("Player"))
			{
				return true;
			}
		}
		return false;
	}
	public bool RayHitDeadNPC(Vector3 direction, float length)
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, direction, out hit, length))
		{
			if(hit.collider.gameObject == DeadNpc.gameObject)
			{
				return true;
			}
		}
		return false;
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
			// have this be a check if we se the player instead
			if (Vector3.Distance(transform.position, player.position) > stats.SightLenght) 
			{ 
				return; 
			} 
		}
		ShouldShout = false;

		SetAlertnessToMax();
	}
	public void ReactToShout(NPC deadNpc)
	{
		if (gameObject.CompareTag("Civilian"))
		{
			// have this be a check if we se the corpse instead
			if (Vector3.Distance(transform.position, player.position) > stats.SightLenght)
			{
				return;
			} 
		}
		DeadNpc = deadNpc;
		ShouldShout = false;
	}

	public void SetAlertnessToMax()
	{
		Alertness = stats.MaxAlerted;
		Run = true;
	}

	public void SetAlertness(float value)
	{
		Alertness = value;

		if(Alertness >= stats.MaxAlerted)
		{
			Run = true;
		}
	}

	public void RaiseAlertness(bool inFOV)
	{
		if(IsCharmed)
		{
			return;
		}
		float value = stats.AlertIncrease * Time.deltaTime;

		//NotoriousLevel
		value += PlayerManager.instance.NotoriousLevel.GetPlayerNotoriousLevel() * stats.AlertIncrease * Time.deltaTime;
		
		if(inFOV)
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

	public void HandleSeeingDeadNPC(NPC deadNpc)
	{
		if(SeesPlayer)
		{
			Run = true;
		}
		else
		{
			DeadNpc = deadNpc;
		}
	}

	public void Dead()
	{
		isDead = true;
		AudioManager.instance.PlaySound(SoundType.CivilianDie, gameObject);
		gameObject.AddComponent<DeadBody>();
		agent.enabled = false;
		Destroy(bloodSuckTarget);
	}

	public void Dispose()
	{
		if (gameObject.CompareTag("Guard"))
		{
			NPCSpawner.Instance.NpcDespawn(false, this);
		}
		else
		{
			NPCSpawner.Instance.NpcDespawn(true, this);
		}
	}
}
