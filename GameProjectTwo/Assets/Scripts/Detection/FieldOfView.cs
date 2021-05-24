using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    [Header("Settings")]
	[SerializeField] LayerMask targetMask;
	[SerializeField] LayerMask npcLayer;

	Player player;

	NPC npc;
	public NPC NPC => npc;

	void Start()
	{
		player = FindObjectOfType<Player>();
		npc = GetComponentInParent<NPC>();
	}
	private void FixedUpdate()
	{
		IncreaseUndetectedTimer();
		FindVisibleCharacters();
	}

	private void IncreaseUndetectedTimer()
	{
		npc.TimeSinceLastSeenPlayer += Time.fixedDeltaTime;
		if (npc.TimeSinceLastSeenPlayer < npc.Stats.CalmDownTime) { return; }
		if (npc.Alertness <= 0) { return; }
		npc.LowerAlertness(npc.Stats.AlertDecrease * Time.fixedDeltaTime);
	}

	void FindVisibleCharacters()
	{
		if (npc.IsDead) { return; }
		DetectVisiblePlayer();
		CheckForDeadNpc();
	}

	private void DetectVisiblePlayer()
	{
		npc.NoticedPlayer = false;
		npc.SeesPlayer = false;

		if(Vector3.Distance(transform.position, player.transform.position) > npc.Stats.SightLenght)
		{
			npc.SawHiding = false;
			npc.SawTransforming = false;
			return;
		}
		PlayerStates playerState = player.CurrentState;

		if (playerState == PlayerStates.DraculaHidden)
		{
			if (npc.SawHiding)
			{
				npc.RaiseAlertness(true);
				npc.SeesPlayer = true;
				npc.NoticedPlayer = true;
				npc.TimeSinceLastSeenPlayer = 0;
			}
			return;
		}
		if (playerState == PlayerStates.BatDefault)
		{
			if (npc.SawTransforming)
			{
				if (!SeesPlayer(player.BatParts))
				{
					return;
				}
				npc.SeesPlayer = true;
				npc.RaiseAlertness(true);
				npc.TimeSinceLastSeenPlayer = 0;
			}
			return;
		}


		Vector3 dirToTarget = (player.transform.position - transform.position).normalized;
		RaycastHit hit;
		if (Vector3.Angle(transform.forward, dirToTarget) < npc.FOV / 2)
		{
			CheckIfSeesPlayer(playerState);
		}
		else if (Physics.Raycast(transform.position, dirToTarget, out hit, npc.Stats.NoticeRange))
		{
			CheckIfInPersonalSpace(playerState, hit);
		}
	}

	private void CheckIfSeesPlayer(PlayerStates playerState)
	{
		Transform[] targets;
		if (playerState == PlayerStates.BatDefault)
		{
			targets = player.BatParts;
		}
		else
		{
			targets = player.BodyParts;
		}
		if (!SeesPlayer(targets))
		{
			return;
		}

		npc.SeesPlayer = true;
		npc.RaiseAlertness(true);
		npc.TimeSinceLastSeenPlayer = 0;

		if (playerState == PlayerStates.DraculaHideing)
		{
			npc.SawHiding = true;
		}
		else if (playerState != PlayerStates.DraculaHidden)
		{
			npc.SawHiding = false;
		}
		if (playerState == PlayerStates.TransformToBat)
		{
			npc.SawTransforming = true;
		}
		else if (playerState != PlayerStates.BatDefault)
		{
			npc.SawTransforming = false;
		}


		if (playerState != PlayerStates.DraculaSucking &&
			playerState != PlayerStates.TransformToDracula &&
			playerState != PlayerStates.TransformToBat &&
			playerState != PlayerStates.DraculaDragBody &&
			playerState != PlayerStates.DraculaBurning)
		{
			return;
		}
		npc.SetAlertnessToMax();
	}

	private void CheckIfInPersonalSpace(PlayerStates playerState, RaycastHit hit)
	{
		if (!hit.collider.CompareTag("Player"))
		{
			npc.NoticedPlayer = false;
			return;
		}
		if (playerState == PlayerStates.DraculaSucking ||
		   playerState == PlayerStates.DraculaBurning)
		{
			npc.SetAlertnessToMax();
		}
		npc.NoticedPlayer = true;
		npc.RaiseAlertness(false);
		npc.TimeSinceLastSeenPlayer = 0;
	}

	private bool SeesPlayer(Transform[] bodyParts)
	{
		RaycastHit hit;

		foreach (var part in bodyParts)
		{
			if (player.CurrentState == PlayerStates.DraculaSucking)
			{
				if (Physics.Linecast(transform.position, part.position, out hit, ~npcLayer))
				{
					if (hit.collider.CompareTag("Player"))
					{
						return true;
					}
				}
			}
			else
			{
				if (Physics.Linecast(transform.position, part.position, out hit))
				{
					if (hit.collider.CompareTag("Player"))
					{
						return true;
					}
				}
			}

		}
		return false;
	}

	private void CheckForDeadNpc()
	{
		Collider[] NearbyNPCs = Physics.OverlapSphere(transform.position, npc.Stats.BodyReactionRange, npcLayer);
		if (NearbyNPCs.Length < 1)
		{
			return;
		}
		foreach (var deadNpc in NearbyNPCs)
		{
			NPC character = deadNpc.gameObject.GetComponentInParent<NPC>();
			
			if(character.CurrentState != NPCStates.Dead)
			{
				continue;
			}

			Transform[] rayPoints = character.BodyParts;
			RaycastHit hit;

			foreach (var point in rayPoints)
			{
				Vector3 dirToTarget = (point.transform.position - transform.position).normalized;
				if (Vector3.Angle(transform.forward, dirToTarget) > npc.FOV)
				{
					continue;
				}
				if (Physics.Linecast(transform.position, point.position, out hit))
				{
					if (!hit.collider.CompareTag("Civilian"))
					{
						continue;
					}
					npc.HandleSeeingDeadNPC(character);
					return;
				}
			}
		}
	}
}