using UnityEngine;

[System.Serializable]
public class PlayerTransition
{
	[Tooltip("The decision to evaluate if we should transition or not")]
	public PlayerDecision decision;
	[Tooltip("The state that we should transition into if decision is true")]
	public PlayerState newState;
}
