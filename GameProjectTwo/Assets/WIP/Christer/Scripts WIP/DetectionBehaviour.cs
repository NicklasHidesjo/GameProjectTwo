using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBehaviour : MonoBehaviour
{

    GameObject LastHit;
    GameObject Player;

    int suspicionMeter = 0;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("CheckForVampires", .2f);
    }

    void Update()
    {


    }

    private void OnDrawGizmos()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, Player.transform.position);
    }
    IEnumerator CheckForVampires(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
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
                suspicionMeter++;
                Debug.Log($"Suspicion increased to {suspicionMeter}");
            }
        }
    }
}
