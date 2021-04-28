using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NpcStats", menuName = "ScriptableObjects/NPCStats")]
public class NPCStats : ScriptableObject
{
	// write a tooltip/comment for what every stat do

	[Header("Other Settings")]
	public int MaxHealth = 100;
	[Tooltip("The distance at where the npc will detect a wall or other obstacle")]
	public float ClearanceDistance = 3f;

	[Header("Detection Settings")]
	[Tooltip("When this level of alertness is reached the character will either flee from or chase the player")]
	public float MaxAlerted = 10f;
	[Tooltip("When alertness reaches this level the character will start looking at the player")]
	public float CautiousThreshold = 2f;
	[Tooltip("When alertness reaches this level the character will start to take more clear actions then looking at the player")]
	public float AlertActionThreshold = 5f;
	[Tooltip("The decrease in alertness (per second)")]
	public float AlertDecrease = 1f;
	[Tooltip("The increase in alertness (per second)")]
	public float AlertIncrease = 1f; // break this into 2 different (one for close but not in sigth and one for insight)?
	[Tooltip("This multiplies AlertIncrease if we are in the characters FOW by its amount")]
	public float InSightMultiplier = 2f;
	[Tooltip("The radius at wich the character will detect the player even if sight blocked (reaction to sounds etc.")]
	public float NoticeRange = 3f;

	[Header("Sight Settings")]
	[Tooltip("How far the character will be able to see in-front of them")]
	public float SightLenght = 10f;
	[Tooltip("The Characters field of view when idle")]
	[Range(0, 360)] public int RelaxedFOV = 40;
	[Tooltip("The character field of view when alerted (suspicious, chasing, searching")]
	[Range(0, 360)] public int AlertedFOV = 60;
	[Tooltip("The angle that the character will pan from and to when searching for the player (forward +- SearchAngle (DO NOT SET THIS HIGHER THEN 89!!!)")]
	[Range(0,89)]public int SearchAngle = 45;

	[Header("Speed Settings")]
	public float SuspisciousSpeed = 0.5f;
	public float WalkSpeed = 3.5f;
	public float RunSpeed = 5f;
	public float SearchSpeed = 1.5f;
	public float FearSpeed = 0.2f;
	[Tooltip("The speed that the character will rotate when searching")]
	public float RotationSpeed = 2;

	[Header("Flee Settings")]
	[Tooltip("The angle that is a dead flee zone behind the player (Lower = character can run closer back towards the player. Higher = straighter running)")]
	[Range(90,180)]public int FleeDeadAngle = 95;
	public float FleeDistance = 2f;
	// these might no longer be used once new flee is implemented.
	public float MaxFleeDistance = 40f;
	public float MinFleeDistance = 5f;

	[Header("Timers")]
	public float IdleTime = 2f;
	[Tooltip("The duration that the Civilian will be stunned after getting their blood sucked")]
	public float SuckedStun = 2f;
	[Tooltip("For how long (in seconds) that the character will be able to guess the new position of the player (based on it's position and it's velocity)")]
	[Range(0.1f,1f)]public float IntuitionTime = 0.5f;
	[Tooltip("The time (in seconds) that it takes for the character to calm down and start decreasing in alertness after noticing the player.")]
	public float CalmDownTime = 5f; 

	[Header("NPC to NPC interaction Settings")]
	[Tooltip("The radius that other NPC's will be notified when getting alerted")]
	public float ShoutRange = 5f;

	[Header("Attack Settings")]
	public int Damage = 10;
	public float AttacksPerSecond = 1f;
}
