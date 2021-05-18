using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
	[SerializeField] PlayerStats stats;

	[SerializeField] GameObject dracula;
	[SerializeField] GameObject bat;

	private PlayerStatsManager statsManager;

	private InteractableScanner iScanner;
	public InteractableScanner IScanner => iScanner;

	private PlayerObjectInteract playerObjectInteract;
	public PlayerObjectInteract PlayerObjectInteract => playerObjectInteract;

	public PlayerStats Stats => stats;

	public PlayerStates CurrentState { get; set; }

	private CharacterController controller;
	public CharacterController Controller => controller;

	public Transform Transform => transform;

	private Transform alingCamera;
	public Transform AlignCamera => alingCamera;

	public float Speed { get; set; }
	public float CurrentStamina { get; set; }

	public bool InSun { get; set; }

	public bool IsDead { get; set; }

	public bool SuckingBlood { get; set; }
	public bool DraggingBody { get; set; }
	public bool Hiding { get; set; }
	public bool StopHiding { get; set; }
	public bool ContainerInteractionDone { get; set; }
	public bool DoneRotating { get; set; }


	public float StateTime { get; set; }

	public bool LeaveBat { get; set; }

	[SerializeField] Transform[] bodyParts;
	public Transform[] BodyParts => bodyParts;

	[SerializeField] Transform[] batParts;
	public Transform[] BatParts => batParts;

	private void Awake()
	{
		controller = GetComponent<CharacterController>();
		statsManager = GetComponent<PlayerStatsManager>();
		iScanner = GetComponent<InteractableScanner>();
		playerObjectInteract = GetComponent<PlayerObjectInteract>();
		alingCamera = Camera.main.transform;

		bat.SetActive(false);
		dracula.SetActive(true);

		CurrentStamina = stats.MaxStamina;
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		LeaveBat = true;
	}

	public void ActivateBatForm()
	{
		dracula.SetActive(false);
		bat.SetActive(true);
	}
	public void ActivateDraculaForm()
	{
		var rotation = transform.rotation;
		rotation.x = 0;
		rotation.z = 0;
		transform.rotation = rotation;

		dracula.SetActive(true);
		bat.SetActive(false);
	}

	public void DecreaseStamina(float decrease)
	{
		CurrentStamina = Mathf.Clamp(CurrentStamina - (decrease * Time.deltaTime), 0, stats.MaxStamina);
		statsManager.SetStaminaBar(CurrentStamina);
	}
	// make this vary based on the state that the player is in (for example Idle will increase stamina faster then walking)
	public void RecoverStamina(float increase)
	{
		CurrentStamina = Mathf.Clamp(CurrentStamina + (increase * Time.deltaTime), 0, stats.MaxStamina);
		statsManager.SetStaminaBar(CurrentStamina);
	}
}
