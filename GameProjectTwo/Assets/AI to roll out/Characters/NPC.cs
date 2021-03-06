using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class NPC : MonoBehaviour, ICharacter
{
    [SerializeField] NPCStats stats;
    [Tooltip("Set this to true if we want the npc to be stationary")]
    [SerializeField] bool stationary;
    [Tooltip("The layer that npc's are at (used when trying to hit player and also when finding other npc's")]
    [SerializeField] LayerMask npcLayer;
    [SerializeField] SpriteRenderer charmInteractionRenderer;
    public LayerMask NpcLayer => npcLayer;

    private NavMeshAgent agent;
    public NavMeshAgent Agent => agent;

    public Transform Transform => transform;
    public NPC Self => this;

    private PathPoint[] path;
    public PathPoint[] Path => path;
    public PathPoint targetPoint { get; set; }

    public NPCStates CurrentState { get; set; }

    private Transform playerTransform;
    public Transform PlayerTransform => playerTransform;

    private Player player;
    public Player Player => player;

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
    public bool RotationStarted { get; set; }
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

    [SerializeField] Transform[] bodyParts;
    public Transform[] BodyParts => bodyParts;

    public bool Leave { get; set; }

    BloodSuckTarget bloodSuckTarget;

    private HiddenCheck hiddenCheck;
    public HiddenCheck HiddenCheck => hiddenCheck;

    public SpawnPath startingPath { get; set; }

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
        player = FindObjectOfType<Player>();
        playerTransform = player.transform;
        bloodSuckTarget = GetComponent<BloodSuckTarget>();
        hiddenCheck = FindObjectOfType<HiddenCheck>();
    }

    public void InitializeNPC(PathPoint[] path = null, bool backTrack = false)
    {
        Destroy(GetComponent<DeadBody>());

        if (gameObject.CompareTag("Civilian"))
        {
            if (bloodSuckTarget == null)
            {
                gameObject.AddComponent<BloodSuckTarget>();
            }
            bloodSuckTarget = GetComponent<BloodSuckTarget>();
        }

        agent.enabled = true;
        this.path = path;
        if (stationary)
        {
            StartingPosition = transform.position;
            StartingRotation = SetStartingRotation();
        }
        DeadNpc = null;
        SetBools(backTrack);
        SetFloatsAndInts();
        SetArrays();
        GetComponent<StateMachine>().InitializeStateMachine();
        SetCharmInteraction(false);
    }

    private Quaternion SetStartingRotation()
    {
        if(gameObject.CompareTag("Civilian"))
		{
            return transform.rotation;
		}

        bool hitLeft = GetHit(-Transform.right);
        bool hitRight = GetHit(Transform.right);
        bool hitFront = GetHit(Transform.forward);

        float yTweak = 0;

        if (hitLeft && hitRight && hitFront)
        {
            yTweak = Transform.rotation.y + 180;
        }
        else if (hitFront && hitLeft)
        {
            yTweak = Transform.rotation.y + 135;
        }
        else if (hitFront && hitRight)
        {
            yTweak = Transform.rotation.y - 135;
        }
        else if (hitRight && hitLeft)
        {
            yTweak = 0;
        }
        else if (hitLeft)
        {
            yTweak = Transform.rotation.y + 90;
        }
        else if (hitRight)
        {
            yTweak = Transform.rotation.y - 90;
        }
        else if (hitFront)
        {
            yTweak = Transform.rotation.y + 180;
        }

        var rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y + yTweak, transform.rotation.z));

        return rotation;
    }
    private bool GetHit(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(Transform.position, direction, out hit, Stats.ClearanceDistance))
        {
            if (hit.collider != null)
            {
                return true;
            }
        }
        return false;
    }


    private void SetBools(bool backTrack)
	{
		IsSuckable = true;
        IsCharmed = false;
		GettingSucked = false;
		isDead = false;
		ShouldShout = true;
		GettingDisposed = false;
		Disposed = false;
		Leave = false;
		Run = false;
        Increase = true;
        if (gameObject.CompareTag("Guard"))
		{
			BackTrack = backTrack;
		}
		else
		{
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
        StateTime = 0;
        player.GetComponent<PlayerStatsManager>().DecreaseHealthValue(stats.Damage);
        player.StopHiding = true;
        player.SuckingBlood = false;
        var dir = playerTransform.position - transform.position;
        player.KnockBack(dir, stats.KnockbackForce);
        AudioManager.instance.PlayOneShot(SoundType.DraculaDamage, player.gameObject);
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
        Quaternion target = Quaternion.LookRotation(playerTransform.position - transform.position);

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
            if (hit.collider.gameObject == DeadNpc.gameObject)
            {
                return true;
            }
        }
        return false;
    }
    public void DecreaseHealth(int health)
    {
        currentHealth -= health;
    }

    public void ReactToShout()
    {
        if (gameObject.CompareTag("Civilian"))
        {
            // have this be a check if we se the player instead
            if (Vector3.Distance(transform.position, playerTransform.position) > stats.SightLenght)
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
            if (Vector3.Distance(transform.position, playerTransform.position) > stats.SightLenght)
            {
                return;
            }
        }
        DeadNpc = deadNpc;
        ShouldShout = false;
    }

    public void SetAlertnessToMax()
    {
        if (IsCharmed)
        {
            return;
        }

        Alertness = stats.MaxAlerted;
        Run = true;
    }

    public void SetAlertness(float value)
    {
        if (IsCharmed)
        {
            return;
        }

        Alertness = value;

        if (Alertness >= stats.MaxAlerted)
        {
            Run = true;
        }
    }

    public void RaiseAlertness(bool inFOV)
    {
        if (IsCharmed)
        {
            return;
        }

        float value = player.GetComponent<PlayerNotoriousLevels>().GetPlayerNotoriousLevel() * stats.AlertIncrease * Time.deltaTime;
        if (inFOV)
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
        if(Alertness < stats.MaxAlerted)
		{
            Run = false;
		}
    }

    public void HandleSeeingDeadNPC(NPC deadNpc)
    {
        if (SeesPlayer)
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
        GettingSucked = false;
        gameObject.AddComponent<DeadBody>();
        AudioManager.instance.PlaySound(SoundType.CivilianDie, gameObject);
        GetComponent<Rigidbody>().isKinematic = false;
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

    public void SetCharmInteraction(bool isCharmTarget)
	{
        if(isCharmTarget)
		{
            charmInteractionRenderer.enabled = true;
		}
        else
		{
            charmInteractionRenderer.enabled = false;
		}
	}
}
