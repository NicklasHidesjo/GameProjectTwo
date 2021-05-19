using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetCharmTarget", menuName = "Player/Action/SetCharmTarget")]
public class SetCharmTarget : PlayerAction
{
	[SerializeField] LayerMask targetMask;

	Transform playerTransform;

	public override void Execute(IPlayer player)
	{
		Debug.Log("starting to set charm");
		if (player.charmTarget != null)
		{
			player.charmTarget.SetCharmInteraction(false);
			player.charmTarget = null;
		}
		if (player.CharmingTarget)
		{
			return;
		}
		if (player.CurrentStamina < player.Stats.CharmStaminaCost)
		{
			return;
		}

		Debug.Log("Did not return so proceeding to set charm");

		playerTransform = player.Transform;

		Collider[] charmTargets = Physics.OverlapSphere(playerTransform.position, player.Stats.CharmRange, targetMask);

		List<NPC> targets = new List<NPC>();

		foreach (var target in charmTargets)
		{
			if (target.CompareTag("Guard"))
			{
				continue;
			}

			NPC npc = target.GetComponent<NPC>();

			if (npc.CurrentState == NPCStates.RunToSafety ||
			   npc.CurrentState == NPCStates.FrozenInFear ||
			   npc.CurrentState == NPCStates.CivFlee ||
			   npc.CurrentState == NPCStates.CivReactToDeadBody ||
			   npc.CurrentState == NPCStates.CivSucked)
			{
				Debug.Log("Found a un charmable npc");
				continue;
			}

			Vector3 dirToTarget = (target.transform.position - playerTransform.position).normalized;

			if (Vector3.Angle(playerTransform.forward, dirToTarget) < player.Stats.CharmFOV / 2)
			{
				RaycastHit hit;
				if (Physics.Raycast(playerTransform.position, dirToTarget, out hit, player.Stats.CharmRange))
				{
					if (hit.collider.CompareTag("Civilian"))
					{
						Debug.Log("Adding a charmable NPC");
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

			Vector3 directionToTarget = closestTarget.transform.position - playerTransform.position;
			float closestDistance = directionToTarget.sqrMagnitude;

			foreach (var target in targets)
			{
				directionToTarget = target.transform.position - playerTransform.position;
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
