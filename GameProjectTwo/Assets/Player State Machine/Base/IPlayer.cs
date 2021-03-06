using UnityEngine;
using UnityEngine.AI;

public interface IPlayer
{
	public PlayerStats Stats { get; }
	public Player MyPlayer { get; }

	public InteractableScanner IScanner { get; }
	public PlayerObjectInteract PlayerObjectInteract { get; }

	public Interactable Interactable { get; set; }
	
	public Quaternion TargetRotation { get; set; }

	public PlayerStates CurrentState { get; set; }

	public CharacterController Controller { get; }

	public Transform Transform { get; }

	public Transform AlignCamera { get; }

	public Transform batModellTransform { get; }

	public float Speed { get; set; }

	public bool InSun { get; set; }

	public bool IsDead { get; set; }

	public bool LeaveBat { get; set; }

	public bool SuckingBlood { get; set; }
	public bool DraggingBody { get; set; }
	public bool Hiding { get; set; }
	public bool StopHiding { get; set; }
	public bool ContainerInteractionDone { get; set; }

	public bool DoneRotating { get; set; }

	public float StateTime { get; set; }

	public float CurrentStamina { get; set; }
    public Quaternion originRot { get; set; }

	public NPC charmTarget { get; set; }
	public bool CharmingTarget { get; set; }

	public void ActivateBatForm();

	public void ActivateDraculaForm();

	public void DecreaseStaminaPerSecond(float decrease);
	public void DecreaseStamina(float decrease);
	public void RecoverStamina(float increase);
}
