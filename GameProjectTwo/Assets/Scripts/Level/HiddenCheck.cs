using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HiddenCheck : MonoBehaviour
{

    [SerializeField] GameObject Staticguard;

    private int notHiddenCorpses;
    public int  NotHiddenCorpses { get { return notHiddenCorpses; } }
    List<Vector3> spawnGuards= new List<Vector3>();

    public List<GameObject> GuardRemovedCorpses = new List<GameObject>();

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

    private void CheckForUnhiddenBodies(int currentlevel)
    {
        notHiddenCorpses = 0;
        spawnGuards = new List<Vector3>();

        GameObject[] deadbodies = GameObject.FindGameObjectsWithTag("Civilian");


		foreach (var deadbody in deadbodies)
		{
            if(deadbody.GetComponent<DeadBody>() == null)
			{
                continue;
			}
            if(deadbody.GetComponent<DeadBody>().IsHidden)
			{
                continue;
			}
            notHiddenCorpses++;
            Debug.Log($"The dead body is at {deadbody.transform.position}");

            NavMeshHit hit;
            if (NavMesh.SamplePosition(deadbody.transform.position, out hit, 4, NavMesh.AllAreas))
            {
                spawnGuards.Add(hit.position);
            }
            deadbody.SetActive(false);
        }

		foreach (var deadbody in GuardRemovedCorpses)
		{
            Debug.Log("doing stuff to removed body");
            notHiddenCorpses++;
            Debug.Log($"The dead body is at {deadbody.transform.position}");

            NavMeshHit hit;
            if (NavMesh.SamplePosition(deadbody.transform.position, out hit, 4, NavMesh.AllAreas))
            {
                spawnGuards.Add(hit.position);
            }
            deadbody.SetActive(false);
        }
        GuardRemovedCorpses.Clear();

/*          for (int i = 0; i<deadbodies.Length; i++)
          {
            if (deadbodies[i].GetComponent<DeadBody>() != null)
            {
                if (deadbodies[i].GetComponent<DeadBody>().IsHidden == false)
                {
                    notHiddenCorpses++;
                    Debug.Log($"The dead body is at {deadbodies[i].transform.position}");



                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(deadbodies[i].transform.position, out hit, 4, NavMesh.AllAreas))
                    {
                        spawnGuards.Add(hit.position);
                    }
                    deadbodies[i].SetActive(false);
                }
            }
          }*/
          Debug.Log($"Number of unhidden bodies is {notHiddenCorpses}");

    }
    private void SpawnGuardsAtMessyKills()
    {
        for (int i = 0; i < spawnGuards.Count; i++)
        {
            if (spawnGuards[i] != null)
            {
                Instantiate(Staticguard,spawnGuards[i], Quaternion.identity);
                Debug.Log($"Spawned Guard at {spawnGuards[i]}");
            }
        }
    }

    public void AddGuardRemovedCorpse(GameObject corpse)
	{
        Debug.Log("Added corpse");
        GuardRemovedCorpses.Add(corpse);
	}
}
