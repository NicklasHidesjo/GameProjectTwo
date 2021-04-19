using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    GameObject Player;
    public float viewRadius;

    [Range(0, 360)] public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    //float suspicionMeter = 0;
    int undetectedTimer = 50;
    int infrontOfEnemyFactor = 2;

    NPC npc;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("FindTargetsWithDelay", .2f);
        npc = GetComponent<NPC>();
        //StartCoroutine("CheckForVampires", .2f);
    }

    private void Update()
    {
        if (npc.Alertness ==0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (npc.Alertness > 0 && npc.Alertness < 50)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }

        else if (npc.Alertness > 50)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    IEnumerator CheckForVampires(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            CheckForVampiresInRange();
        }

    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                   // Debug.Log("Player visible in front of Enemy!");
                }
            }
        }
        if(targetsInViewRadius.Length > 0)
        {
            CheckForVampiresInRange();

        }
        if (undetectedTimer < 50)
        {
            undetectedTimer++;
        }
        if (undetectedTimer == 50)
        {
           // Debug.Log("Undetected and suspicion decreasing");
            if (npc.Alertness > 0)
            {
                npc.Alertness--;
            }
        }

    }

    void CheckForVampiresInRange()
    {
        Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.red, (float)0.2);
        var ray = new Ray(transform.position, Player.transform.position - transform.position);
        RaycastHit Hit;

        if (Physics.Raycast(ray, out Hit))
        {
            if (Hit.transform.gameObject.tag == "Player")
            {
                if (visibleTargets.Count > 0)
                {
                    npc.Alertness += infrontOfEnemyFactor;
                    //Debug.Log($"Player visible in front of Enemy. Suspicion increased by {infrontOfEnemyFactor} to {suspicionMeter}");
                    undetectedTimer = 0;
                }
                else
                {
                    npc.Alertness++;
                    //Debug.Log($"Suspicion increased to {suspicionMeter}");
                    undetectedTimer = 0;
                }
            }
        }

    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}