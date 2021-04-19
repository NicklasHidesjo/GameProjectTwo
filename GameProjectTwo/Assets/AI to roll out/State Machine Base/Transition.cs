using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
	[Tooltip("The decision to evaluate if we should transition or not")]
	public Decision decision;
	[Tooltip("The state that we should transition into if decision is true")]
	public State newState;
}
