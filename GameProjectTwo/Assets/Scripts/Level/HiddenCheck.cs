using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HiddenCheck : MonoBehaviour
{

    [SerializeField] GameObject Staticguard;

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
            NavMeshHit hit;
            if (NavMesh.SamplePosition(deadbody.transform.position, out hit, 4, NavMesh.AllAreas))
            {
                spawnGuards.Add(hit.position);
            }
            deadbody.SetActive(false);
        }

		foreach (var deadbody in GuardRemovedCorpses)
		{

            NavMeshHit hit;
            if (NavMesh.SamplePosition(deadbody.transform.position, out hit, 4, NavMesh.AllAreas))
            {
                spawnGuards.Add(hit.position);
            }
            deadbody.SetActive(false);
        }
        GuardRemovedCorpses.Clear();
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
