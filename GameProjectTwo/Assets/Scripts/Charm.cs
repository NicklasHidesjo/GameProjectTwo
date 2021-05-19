using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charm : MonoBehaviour
{
	[SerializeField] LayerMask targetMask;

	Player player;

	private void Start()
	{
		player = GetComponentInParent<Player>();
	}

	private void FixedUpdate()
	{
		if(player.charmTarget != null)
		{
			player.charmTarget.SetCharmInteraction(false);
			player.charmTarget = null;
		}
		if(player.CharmingTarget)
		{
			return;
		}
		if(player.CurrentStamina < player.Stats.CharmStaminaCost)
		{
			return;
		}

		Collider[] charmTargets = Physics.OverlapSphere(transform.position, player.Stats.CharmRange, targetMask);

		List<NPC> targets = new List<NPC>();

		foreach (var target in charmTargets)
		{
			if(target.CompareTag("Guard"))
			{
				continue;
			}

			NPC npc = target.GetComponent<NPC>();

			if(npc.CurrentState == NPCStates.RunToSafety ||
			   npc.CurrentState == NPCStates.FrozenInFear ||
			   npc.CurrentState == NPCStates.CivFlee ||
			   npc.CurrentState == NPCStates.CivReactToDeadBody ||
			   npc.CurrentState == NPCStates.CivSucked)
			{
				continue;
			}

			Vector3 dirToTarget = (target.transform.position - transform.position).normalized;

			Debug.DrawRay(transform.position, dirToTarget);

			if (Vector3.Angle(transform.forward, dirToTarget) < player.Stats.CharmFOV / 2)
			{
				RaycastHit hit;
				if(Physics.Raycast(transform.position, dirToTarget, out hit,player.Stats.CharmRange))
				{
					if(hit.collider.CompareTag("Civilian"))
					{
						targets.Add(npc);
					}
				}

			}
		}

		if (targets.Count < 1)
		{
			player.charmTarget = null;
		}
		else if (targets.Count == 1)
		{
			player.charmTarget = targets[0];
			targets[0].SetCharmInteraction(true);
		}

		else
		{
			NPC closestTarget = targets[0];

			Vector3 directionToTarget = closestTarget.transform.position - transform.position;
			float closestDistance = directionToTarget.sqrMagnitude;

			foreach (var target in targets)
			{
				directionToTarget = target.transform.position - transform.position;
				if (directionToTarget.sqrMagnitude < closestDistance)
				{
					closestDistance = directionToTarget.sqrMagnitude;
					closestTarget = target;
				}
			}
			player.charmTarget = closestTarget;
			closestTarget.SetCharmInteraction(true);
		}
	}
}
