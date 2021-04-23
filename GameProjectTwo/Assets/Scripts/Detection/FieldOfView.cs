using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    private float viewRadius;
    public float ViewRadius => viewRadius;

    private float viewAngle;
    public float ViewAngle => viewAngle;

    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    float undetectedTimer;

    float deathTimer;

    NPC npc;

    void Start()
    {
        npc = GetComponent<NPC>();
        viewRadius = npc.Stats.SightLenght;
        viewAngle = npc.Stats.FieldOfView;
    }
    private void FixedUpdate()
    {
        IncreaseUndetectedTimer();
        FindVisibleTargets();
        ToggleSymbols();
    }

    private void IncreaseUndetectedTimer()
    {
        undetectedTimer += Time.fixedDeltaTime;
        if (undetectedTimer < npc.Stats.CalmDownTime) { return; }
        if (npc.Alertness <= 0) { return; }
        npc.LowerAlertness(npc.Stats.AlertDecrease * Time.fixedDeltaTime);
    }
    private void ToggleSymbols()
    {
        if (npc.IsDead && deathTimer > 5.0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (npc.IsDead)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
            deathTimer += Time.deltaTime;
            return;
        }
        if (npc.Alertness <= npc.Stats.CautiousThreshold)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (npc.Alertness >= npc.Stats.AlertActionThreshold)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (npc.Alertness >= npc.Stats.MaxAlerted)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }
    void FindVisibleTargets()
    {
        Collider[] playersDetected = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (playersDetected.Length < 1) { return; }

        undetectedTimer = 0;

        foreach (var player in playersDetected)
        {
            Vector3 dirToTarget = (player.transform.position - transform.position).normalized;
            RaycastHit hit;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                if (!Physics.Raycast(transform.position, dirToTarget, out hit, npc.Stats.SightLenght))
                {
                    return;
                }
                if (!hit.collider.CompareTag("Player"))
                {
                    return;
                }
                npc.RaiseAlertness(true);
                if (PlayerManager.instance.PlayerState.CurrentState != PlayerState.playerStates.Sucking)
                {
                    return;
                }
                npc.SetAlertnessToMax();
            }
            else if (Physics.Raycast(transform.position, dirToTarget, out hit, npc.Stats.NoticeRange))
            {
                if (!hit.collider.CompareTag("Player"))
                {
                    return;
                }
                npc.RaiseAlertness(true);
            }
        }
    }
}