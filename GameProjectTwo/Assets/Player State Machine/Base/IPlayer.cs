using UnityEngine;
using UnityEngine.AI;

public interface IPlayer
{
	public PlayerStats Stats { get; }

	public PlayerStates currentState { get; set; }

	public CharacterController Controller { get; }

	public Transform Transform { get; }

	public Transform AlignCamera { get; }

	public float Speed { get; set; }

	public bool InSun { get; set; }

	public bool IsDead { get; set; }

	public bool LeaveBat { get; set; }

	public float StateTime { get; set; }

	public float CurrentStamina { get; set; }

	public void ActivateBatForm();

	public void ActivateDraculaForm();

	public void DecreaseStamina(float decrease);
	public void RecoverStamina(float increase);
}
