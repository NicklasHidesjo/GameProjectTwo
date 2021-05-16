using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
	[SerializeField] LayerMask checkLayers;

	[Tooltip("This determines if the npc should go idle once it reaches this point")]
	[SerializeField] bool idlePoint;
	public bool IdlePoint => idlePoint;

	private void Start()
	{
		GetComponent<MeshRenderer>().enabled = false;
	}

	public Vector3 GetPosition()
	{
		Vector3 pos = transform.position;
		Vector3 bounds = GetComponent<Renderer>().bounds.extents;
		pos += new Vector3(Mathf.Lerp(-bounds.x, bounds.x, Random.value), 0, Mathf.Lerp(-bounds.z, bounds.z, Random.value));

		RaycastHit hit;
		if (Physics.Raycast(pos + Vector3.up * 100, Vector3.down, out hit, 200, checkLayers, QueryTriggerInteraction.Ignore))
		{
			pos.y = hit.point.y + 1;
		}

		return pos;
	}
}
