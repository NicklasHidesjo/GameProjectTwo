using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Time settings")]
    public float TransformDuration = 0.5f;
    [Tooltip("The time in seconds it takes to interact with bodies")]
    public float InteractionTime = 1f;

    [Header("Physics settings ")]
    public float Mass = 3f;
    public float Drag = 5f;
    public float Gravity = 20f;
    [Header("Fly physics settings")]
    public float Damping = 2;
    public float DownForce = 10;


    [Header("Stamina settings")]
    public float MaxStamina = 100f;
    public float StaminaRecovery = 10f;
    [Tooltip("The cost for flying per second")]
    public float FlyStaminaCost = 40f;
    [Tooltip("The cost for running per second")]
    public float RunStaminaCost = 10f;
    [Tooltip("The cost for charming a npc")]
    public float CharmStaminaCost = 70f;

    [Header("Speed settings")]
	public float WalkSpeed = 4f;
	public float RunSpeed = 8f;
    public float CrouchSpeed = 1.0f;
	public float DragBodySpeed = 2f;
	public float InSunSpeed = 2f;
    public float FlySpeed = 5f;

    [Header("Fly settings")]
    public float FlightSpeed = 5.0f;
    public float TurnSpeed = 2.0f;
    public float FlightHight = 2.0f;
    public float MaxFlightHight = 4.0f;
    public float MinFlightHight = 2.0f;
    public LayerMask CheckLayerForFlight;

    [Header("Charm settings")]
    public float CharmRange = 10f;
    [Tooltip("The angle that the player can charm a npc infront of them")]
    public float CharmFOV = 90f;
}
