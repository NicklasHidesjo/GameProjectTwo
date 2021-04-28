using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
	private DistrictManager districtManager;
	private EndLevelCheck endLevelCheck;
	[SerializeField] GameObject civilian;
	[SerializeField] GameObject guard;
	[SerializeField] Transform[] spawnLocations;

	[SerializeField] private GameObject[] districts;
	int civiliansActive;

	[SerializeField] private int civilianPoolSize = 5;
	[SerializeField] private int guardPoolSize = 5;
	
	// Start is called before the first frame update
	void Start()
	{
		
		endLevelCheck = GameObject.FindGameObjectWithTag("Lair").GetComponent<EndLevelCheck>();
		NpcPoolManager.Instance.CreatePool(civilian, civilianPoolSize);
		NpcPoolManager.Instance.CreatePool(guard, guardPoolSize);
		
		//StartCoroutine(CivilianSpawn());
		
	}

	// Update is called once per frame
	void Update()
	{
		foreach (GameObject district in districts)
		{
			
		}
		
		//All IF-statements below in Update are for testing purposes
		if (Input.GetKeyDown(KeyCode.K))
		{
			//NpcDespawnCivilian(GameObject.FindWithTag("Civilian"));
		}

		//if (civiliansActive < (endLevelCheck.LevelPassedThreshold[endLevelCheck.CurrentLevel] / 100) * 2)
		if (Input.GetKeyDown(KeyCode.I))
		{
			//StartCoroutine(CivilianSpawn());
			NpcSpawnCivilian(civilian, districts[1]);
		}
	}

	IEnumerator CivilianSpawn()
	{
		int i = Random.Range(0, spawnLocations.Length);
		Transform currentSpawn = spawnLocations[i];
		NpcPoolManager.Instance.ReuseNpc(civilian, currentSpawn);
		yield return new WaitForSeconds(3.0f);

		if (civiliansActive < civilianPoolSize)
		{
			civiliansActive++;
		}
	}

	public void NpcSpawnCivilian(GameObject npc, GameObject district)
	{
		districtManager = district.GetComponent<DistrictManager>();
		int i = Random.Range(0, districtManager.GetCivilianSpawnPoints().Length);
		Transform currentSpawn = districtManager.GetCivilianSpawnPoints()[i];
		NpcPoolManager.Instance.ReuseNpc(npc, currentSpawn);
	}

	public void NpcDespawnCivilian(GameObject npc, GameObject district)
	{
		npc.SetActive(false);

		if (npc.CompareTag("Civilian"))
		{
			civiliansActive--;
			Debug.Log("Civilians active: " + civiliansActive);
		}
	}
}