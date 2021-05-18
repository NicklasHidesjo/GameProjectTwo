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

    [Header("Stamina settings")]
    public float MaxStamina = 100f;
    public float StaminaRecovery = 10f;
    [Tooltip("The cost for flying per second")]
    public float FlyStaminaCost = 40;
    [Tooltip("The cost for running per second")]
    public float RunStaminaCost = 10;

    [Header("Speed settings")]
	public float WalkSpeed = 4f;
	public float RunSpeed = 8f;
    public float CrouchSpeed = 1.0f;
	public float DragBodySpeed = 2f;
	public float InSunSpeed = 2f;
    public float FlySpeed = 5f;

    [Header("Fly settings")]
    [Header("InputSettings")]
    public float flightSpeed = 5.0f;
    public float turnSpeed = 2.0f;
    public float flightHight = 2.0f;
    public float maxFlightHight = 4.0f;
    public float minFlightHight = 2.0f;


    [Header("PhysicsSettings")]
    public float damping = 2;
    public float downForce = 10;
    public LayerMask checkLayerForFlight;
}
