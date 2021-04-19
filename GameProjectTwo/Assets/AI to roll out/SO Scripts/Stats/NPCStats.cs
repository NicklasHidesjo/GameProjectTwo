using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NpcStats", menuName = "ScriptableObjects/NPCStats")]
public class NPCStats : ScriptableObject
{
	[Header("Shared stats")]
	public float ChangeDuration = 2f;
	public float SightLenght = 10f;
	public float MaxAlerted = 10f;
	public float CautiousThreshold = 2f;
	public float AlertDecrease = 1;
	public float AlertIncrease = 1;
	public float IdleTime = 2f;

	[Tooltip("The angle that the player will pan from and to when searching for player (forward +- SearchAngle")]
	public int SearchAngle = 90; // make this be a slider value between 45 - 180? 

	public float SuspisciousSpeed = 0.5f;
	public float WalkSpeed = 3.5f;
	public float RunSpeed = 5f;

	[Header("Civilian only stats")]
	public float FleeAngle = 45f; // make this be a slider value between 10 - 180? 
	public float MaxFleeDistance = 40f;
	public float MinFleeDistance = 5f;
	public float BackAwaySpeed = 0.5f; // make slider value between .01 - .1 ?

	[Header("Guard only stats")]
	public float FollowRange = 10f;
	public float RotationSpeed = 30f;
	public float AttacksPerSecond = 1f;

	public float SuspicionTime = 2f;
	public float IntuitionTime = 1f;
}
