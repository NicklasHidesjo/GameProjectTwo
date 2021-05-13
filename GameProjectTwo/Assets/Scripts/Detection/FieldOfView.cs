using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    [Header("Settings")]
	[SerializeField] LayerMask targetMask;
	[SerializeField] LayerMask npcLayer;

	[Header("ConeCastFromToHeads")]
	[SerializeField] int segmentsInCone = 4;
	[SerializeField] float plHeadRadius = 0.45f;
	[SerializeField] float plNeckHight = 0.5f;
	[SerializeField] float nPCNeckHight = 0.5f;



	NPC npc;
	public NPC NPC => npc;

	void Start()
	{
		npc = GetComponent<NPC>();
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
		Collider[] playersDetected = Physics.OverlapSphere(transform.position, npc.Stats.SightLenght, targetMask);
		npc.NoticedPlayer = false;
		npc.SeesPlayer = false;

		if (playersDetected.Length < 1)
		{
			npc.SawHiding = false;
			npc.SawTransforming = false;
			return;
		}
		foreach (var player in playersDetected)
		{
			PlayerStates playerState = PlayerManager.instance.PlayerState.CurrentState;

			if (playerState == PlayerStates.DraculaHidden)
			{
				if (npc.SawHiding)
				{
					npc.RaiseAlertness(true);
					npc.SeesPlayer = true;
					npc.NoticedPlayer = true;
					npc.TimeSinceLastSeenPlayer = 0;
				}
				continue;
			}
			if (playerState == PlayerStates.BatDefault)
			{
				if (npc.SawTransforming)
				{
					npc.RaiseAlertness(true);
					npc.SeesPlayer = true;
					npc.NoticedPlayer = true;
					npc.TimeSinceLastSeenPlayer = 0;
				}
				continue;
			}


			Vector3 dirToTarget = (player.transform.position - transform.position).normalized;
			RaycastHit hit;
			if (Vector3.Angle(transform.forward, dirToTarget) < npc.FOV / 2)
			{
				//Robert was here!
				bool seesPlayer = ConeCast(transform.position, player.transform.position, npc.Stats.SightLenght);
				if (!seesPlayer)
				{
					continue;
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
			else if (Physics.Raycast(transform.position, dirToTarget, out hit, npc.Stats.NoticeRange))
			{
				if (!hit.collider.CompareTag("Player"))
				{
					npc.NoticedPlayer = false;
					return;
				}
				npc.NoticedPlayer = true;
				npc.RaiseAlertness(false);
				npc.TimeSinceLastSeenPlayer = 0;
			}
		}
	}
	private bool ConeCast(Vector3 from, Vector3 to, float rayLength)
	{
		Vector3 fromToEye = from + Vector3.up * nPCNeckHight;
		Vector3 toToHead = to + Vector3.up * plNeckHight;
		Vector3 dir = fromToEye - toToHead;

		float radius = plHeadRadius;
		int numberOfRays = segmentsInCone;
		float angStep = 360 / segmentsInCone;

		RaycastHit hit;
		for (int i = 0; i < numberOfRays; i++)
		{
			Vector3 offDir = (Quaternion.LookRotation(dir) * Quaternion.Euler(0, 0, angStep * i)) * (Vector3.up * radius);
			Debug.DrawLine(fromToEye, toToHead + offDir, Color.magenta);

			if (Physics.Linecast(fromToEye, toToHead + offDir, out hit))
			{
				if (hit.collider.CompareTag("Player"))
				{
					Debug.DrawRay(fromToEye, toToHead + offDir, Color.red);
					return true;
				}

			}
		}
		return false;
	}

	private void CheckForDeadNpc()
	{
		Collider[] NearbyNPCs = Physics.OverlapSphere(transform.position, npc.Stats.SightLenght, npcLayer);
		if (NearbyNPCs.Length < 1)
		{
			return;
		}
		foreach (var deadNpc in NearbyNPCs)
		{
			NPC character = deadNpc.gameObject.GetComponentInParent<NPC>();
			if (!character.IsDead)
			{
				return;
			}

			Transform[] rayPoints = character.BodyParts;

			RaycastHit hit;

			foreach (var point in rayPoints)
			{
				Vector3 dirToTarget = (point.transform.position - transform.position).normalized;
				if (Vector3.Angle(transform.forward, dirToTarget) > npc.FOV / 2)
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