using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    [Header("DEBUG")]

    [SerializeField] bool debug = true;
    GameObject debugSphere;
    float sphareScale = 1;

    [Header("Settings")]
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    [Header("ConeCastFromToHeads")]
    [SerializeField] int segmentsInCone = 4;
    [SerializeField] float plHeadRadius = 0.45f;
    [SerializeField] float plNeckHight = 0.5f;
    [SerializeField] float nPCNeckHight = 0.5f;

    float deathTimer;

    NPC npc;
    public NPC NPC => npc;


    void Start()
    {
        npc = GetComponent<NPC>();
    }
    private void FixedUpdate()
    {
        IncreaseUndetectedTimer();
        FindVisibleTargets();
        ToggleSymbols();
    }

    private void IncreaseUndetectedTimer()
    {
        npc.TimeSinceLastSeenPlayer += Time.fixedDeltaTime;
        if (npc.TimeSinceLastSeenPlayer < npc.Stats.CalmDownTime) { return; }
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
        else if (npc.Alertness >= npc.Stats.MaxAlerted)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }

    }
    void FindVisibleTargets()
    {
        float notoriusLevel = PlayerManager.instance.NotoriousLevel.GetPlayerNotoriousLevel();
        float sightRange = Mathf.Lerp(npc.Stats.SightLenght, npc.Stats.SightLenght * 2, notoriusLevel);
        float noticeRange = Mathf.Lerp(npc.Stats.NoticeRange, npc.Stats.NoticeRange * 2, notoriusLevel);
        // Mathf.Lerp(MinRange, MaxRange, notoriusLevel);


        if (debug)
        {
            DebugNotisRange(sightRange, noticeRange);
        }
        if (npc.IsDead) { return; }
        Collider[] playersDetected = Physics.OverlapSphere(transform.position, sightRange, targetMask);

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
            PlayerState.playerStates playerState = PlayerManager.instance.PlayerState.CurrentState;

            if (playerState == PlayerState.playerStates.DraculaHidden)
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
            if (playerState == PlayerState.playerStates.BatDefault)
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
                bool seesPlayer = ConeCast(transform.position, player.transform.position, sightRange);
                if (!seesPlayer)
                {
                    return;
                }
                /*
                if (!Physics.Raycast(transform.position, dirToTarget, out hit, npc.Stats.SightLenght))
                {
                    return;
                }
                if (!hit.collider.CompareTag("Player"))
                {
                    return;
                }
                */
                npc.SeesPlayer = true;
                npc.RaiseAlertness(true);
                npc.TimeSinceLastSeenPlayer = 0;

                if (playerState == PlayerState.playerStates.DraculaHideing)
                {
                    npc.SawHiding = true;
                }
                else if (playerState != PlayerState.playerStates.DraculaHidden)
                {
                    npc.SawHiding = false;
                }
                if (playerState == PlayerState.playerStates.TransformToBat)
                {
                    npc.SawTransforming = true;
                }
                else if (playerState != PlayerState.playerStates.BatDefault)
                {
                    npc.SawTransforming = false;
                }


                if (playerState != PlayerState.playerStates.DraculaSucking &&
                    playerState != PlayerState.playerStates.TransformToDracula &&
                    playerState != PlayerState.playerStates.TransformToBat &&
                    playerState != PlayerState.playerStates.DraculaDragBody &&
                    playerState != PlayerState.playerStates.DraculaBurning)
                {
                    return;
                }
                npc.SetAlertnessToMax();
            }
            else if (Physics.Raycast(transform.position, dirToTarget, out hit, noticeRange))
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

    bool ConeCast(Vector3 from, Vector3 to, float rayLength)
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
                if (hit.collider.tag == "Player")
                {
                    Debug.DrawRay(fromToEye, toToHead + offDir, Color.red);
                    return true;
                }

            }
        }
        return false;
    }


    void DebugNotisRange(float notis, float sight)
    {
        if (!debugSphere)
        {
            debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Destroy(debugSphere.GetComponent<Collider>());
            debugSphere.GetComponent<Renderer>().material = PlayerManager.instance.NotoriousLevel.debugMat;
        }
        float scale = sight;
        if (notis > sight)
        {
            scale = notis;
        }
        debugSphere.transform.position = transform.position;
        debugSphere.transform.localScale = Vector3.one * scale;

    }
}