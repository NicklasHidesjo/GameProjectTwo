using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NpcStats", menuName = "ScriptableObjects/NPCStats")]
public class NPCStats : ScriptableObject
{
	// write a tooltip/comment for what every stat do

	[Header("Shared stats")]
	public float ChangeDuration = 2f;
	public float SightLenght = 10f;
	public float MaxAlerted = 10f;
	public float CautiousThreshold = 2f;
	public float AlertDecrease = 1;
	public float AlertIncrease = 1;
	public float IdleTime = 2f;

	[Tooltip("The angle that the player will pan from and to when searching for player (forward +- SearchAngle (DO NOT SET THIS HIGHER THEN 89!!!)")]
	[Range(0,89)]public int SearchAngle = 45; 

	public float SuspisciousSpeed = 0.5f;
	public float WalkSpeed = 3.5f;
	public float RunSpeed = 5f;
	public float SearchSpeed = 1.5f;

	[Header("Civilian only stats")]
	public float FleeAngle = 45f; // make this be a slider value between 10 - 180? 
	public float MaxFleeDistance = 40f;
	public float MinFleeDistance = 5f;
	public float BackAwaySpeed = 0.5f; // make slider value between .01 - .1 ?

	[Header("Guard only stats")]
	public int Damage = 10;
	public float FollowRange = 10f;
	public float RotationSpeed = 2;
	[Tooltip("The distance at where the guard will detect a wall or other obstacle(used when searching)")]
	public float ClearanceDistance = 3f;
	public float AttacksPerSecond = 1f;


	public float SuspicionTime = 2f; // this is used for the standing still searching
 	public float IntuitionTime = 1f; // this i might remove and replace with another system.

	public float SearchRadius = 3f; // this might be removed
}
