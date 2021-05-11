using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGenerator : MonoBehaviour
{
	[Tooltip("This determines if the npc should go idle once it reaches this point")]
	[SerializeField] bool idlePoint;
	public bool IdlePoint => idlePoint;
}
