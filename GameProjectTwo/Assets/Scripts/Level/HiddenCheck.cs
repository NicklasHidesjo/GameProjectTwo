using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenCheck : MonoBehaviour
{

    [SerializeField] GameObject Staticguard;

    private int notHiddenCorpses;
    public int  NotHiddenCorpses { get { return notHiddenCorpses; } }
    List<Transform> spawnGuards= new List<Transform>();

    private EndLevelCheck endlevelcheck;
    private MenuManager menuManager;
    // Start is called before the first frame update
    void Start()
    {

        endlevelcheck = gameObject.GetComponent<EndLevelCheck>();
        menuManager = FindObjectOfType<MenuManager>();

        EndLevelCheck.OnLevelEnded += CheckForUnhiddenBodies;
        MenuManager.OnLevelStart += SpawnGuardsAtMessyKills;

    }

    void CheckForUnhiddenBodies(int currentlevel)
    {
        notHiddenCorpses = 0;
        spawnGuards = new List<Transform>();

        GameObject[] deadbodies = GameObject.FindGameObjectsWithTag("Civilian");
               

          for (int i = 0; i<deadbodies.Length; i++)
          {
                if (deadbodies[i].GetComponent<DeadBody>() != null)
                {
                    if (deadbodies[i].GetComponent<DeadBody>().IsHidden == false)
                    {
                    notHiddenCorpses++;
                    Debug.Log($"The dead body is at {deadbodies[i].transform.position}");
                    spawnGuards.Add(deadbodies[i].transform);
                    deadbodies[i].SetActive(false);
                    }
                }
                else
                {
                continue;
                }

          }
          Debug.Log($"Number of unhidden bodies is {notHiddenCorpses}");

    }
    void SpawnGuardsAtMessyKills()
    {
        for (int i = 0; i < spawnGuards.Count; i++)
        {
            if (spawnGuards[i] !=null)
            {
                Instantiate(Staticguard,spawnGuards[i].transform.position, Quaternion.identity);
                Debug.Log($"Spawned Guard at {spawnGuards[i].transform.position}");
            }
        }
    }
}
