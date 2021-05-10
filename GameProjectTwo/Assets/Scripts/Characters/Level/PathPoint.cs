using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint
{
	private bool idlePoint;

	public bool IdlePoint => idlePoint;

	private Vector3 position;

	public Vector3 Position => position;

	public PathPoint(Vector3 position, bool idlePoint)
	{
		this.position = position;
		this.idlePoint = idlePoint;
	}
}
