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

	// Start is called before the first frame update
	void Start()
	{
		endLevelCheck = GameObject.FindGameObjectWithTag("Lair").GetComponent<EndLevelCheck>();
		NpcPoolManager.Instance.CreatePool(civilian, 5);

		while (civiliansActive < (endLevelCheck.LevelPassedThreshold[endLevelCheck.CurrentLevel] / 100) * 2)
		{
			StartCoroutine(CivilianSpawn());
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			//StartCoroutine(CivilianSpawn());
			NpcDespawn(GameObject.FindWithTag("Civilian"));
		}
	}

	IEnumerator CivilianSpawn()
	{
		int i = Random.Range(0, spawnLocations.Length);
		Transform currentSpawn = spawnLocations[i];
		NpcPoolManager.Instance.ReuseNpc(civilian, currentSpawn);
		yield return new WaitForSeconds(3.0f);
		civiliansActive++;
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