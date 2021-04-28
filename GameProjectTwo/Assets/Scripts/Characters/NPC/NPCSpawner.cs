using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
	private EndLevelCheck endLevelCheck;
	[SerializeField] GameObject civilian;
	[SerializeField] GameObject guard;
	[SerializeField] Transform[] spawnLocations;
	int civiliansActive;

	[SerializeField] private int civilianPoolSize = 5;

	// Start is called before the first frame update
	void Start()
	{
		endLevelCheck = GameObject.FindGameObjectWithTag("Lair").GetComponent<EndLevelCheck>();
		NpcPoolManager.Instance.CreatePool(civilian, civilianPoolSize);
		
		//StartCoroutine(CivilianSpawn());
		
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			NpcDespawn(GameObject.FindWithTag("Civilian"));
		}

		//if (civiliansActive < (endLevelCheck.LevelPassedThreshold[endLevelCheck.CurrentLevel] / 100) * 2)
		if (Input.GetKeyDown(KeyCode.I))
		{
			StartCoroutine(CivilianSpawn());
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

	public void NpcDespawn(GameObject npc)
	{
		npc.SetActive(false);

		if (npc.CompareTag("Civilian"))
		{
			civiliansActive--;
			Debug.Log("Civilians active: " + civiliansActive);
		}
	}
}