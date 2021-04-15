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

    int suspicionMeter = 0;
    int infrontOfEnemyFactor = 2;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("FindTargetsWithDelay", .2f);
        //StartCoroutine("CheckForVampires", .2f);
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
                    Debug.Log("Player visible in front of Enemy!");
                }
            }
        }
        if(targetsInViewRadius.Length > 0)
        {
            CheckForVampiresInRange();
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
                    suspicionMeter += infrontOfEnemyFactor;
                    Debug.Log($"Player visible in front of Enemy. Suspicion increased by {infrontOfEnemyFactor} to {suspicionMeter}");
                }
                else
                {
                    suspicionMeter++;
                    Debug.Log($"Suspicion increased to {suspicionMeter}");
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