using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NpcStats", menuName = "ScriptableObjects/NPCStats")]
public class NPCStats : ScriptableObject
{
	// write a tooltip/comment for what every stat do

	[Header("Other Settings")]
	public int MaxHealth = 100;
	[Tooltip("The distance at where the npc will detect a wall or other obstacle")]
	public float ClearanceDistance = 3f;
	[Tooltip("The minimum allowed degree difference when looking at player before it looks on.")]
	public int HardLockOnAngle = 10;


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
	[Tooltip("The amount of alertness that Npc's will get when leaving a charmed state (and not seeing the player)")]
	public float ExitCharmIncrease = 20f;

	[Tooltip("The range at where the Npc will react to a dead body")]
	public float BodyReactionRange = 10f;

	[Header("Sight Settings")]
	[Tooltip("How far the character will be able to see in-front of them")]
	public float SightLenght = 10f;
	[Tooltip("The Characters field of view when idle")]
	[Range(0, 360)] public int RelaxedFOV = 40;
	[Tooltip("The character field of view when alerted (suspicious, chasing, searching")]
	[Range(0, 360)] public int AlertedFOV = 60;
	[Tooltip("The angle that the character will pan from and to when searching for the player (forward +- SearchAngle (DO NOT SET THIS HIGHER THEN 89!!!)")]
	[Range(0, 89)] public int SearchAngle = 45;

	[Header("Speed Settings")]
	public float SuspisciousSpeed = 0.5f;
	public float WalkSpeed = 3.5f;
	public float RunSpeed = 5f;
	public float SearchSpeed = 1.5f;
	public float FearSpeed = 0.2f;
	public float CharmedSpeed = 1f;
	[Tooltip("The speed that the character will rotate when searching")]
	public float SearchRotationSpeed = 2f;
	[Tooltip("The speed that the character will turn towards the player")]
	public float TurnSpeed = 2f;
	[Tooltip("A crude way of smoothening out the speed when lerping for turning towards player (if the angle difference is larger then this no speed increase will happen)")]
	[Range(0, 50)] public int TurnSpeedCompensation = 45;

	[Header("Flee Settings")]
	[Tooltip("The angle that is a dead flee zone behind the player (Lower = character can run closer back towards the player. Higher = straighter running)")]
	[Range(90, 180)] public int FleeDeadAngle = 95;
	[Tooltip("The maximum distance that the npc will run before changing the direction")]
	public float MaxFleeDistance = 40f;
	[Tooltip("The minimum distance that the npc will run before changing the direction")]
	public float MinFleeDistance = 5f;

	[Header("Timers")]
	public float IdleTime = 2f;
	[Tooltip("For how long (in seconds) that the character will be able to guess the new position of the player (based on it's position and it's velocity)")]
	[Range(0.1f, 2f)] public float IntuitionTime = 0.5f;
	[Tooltip("The time (in seconds) that it takes for the character to calm down and start decreasing in alertness after noticing the player.")]
	public float CalmDownTime = 5f;
	[Tooltip("The time (in seconds) that the npc will stay in a charmed state once charmed by the player")]
	public float CharmedTime = 5f;
	[Tooltip("The time (in seconds) that the npc will take to fade out")]
	public float FadeDuration = 1f;

	[Header("NPC to NPC interaction Settings")]
	[Tooltip("The radius that other NPC's will be notified when getting alerted")]
	public float ShoutRange = 5f;
	[Tooltip("The radius that other NPC's will be notified when getting sucked (if Npc is suspiscious")]
	public float AttackedShoutRange = 2f;

	[Header("Attack Settings")]
	public int Damage = 10;
	public float AttacksPerSecond = 1f;
	public float KnockbackForce = 5f;
}
