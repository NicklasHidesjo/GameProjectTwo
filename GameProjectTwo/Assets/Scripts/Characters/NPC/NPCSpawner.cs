using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
	[SerializeField] GameObject civilian;
	[SerializeField] GameObject guard;
	[SerializeField] Transform[] spawnLocations;
	
	private int civiliansActive;
	private int guardsActive;

	[SerializeField] private int civilianPoolSize = 5;
	[SerializeField] private int guardPoolSize = 5;

	private static NPCSpawner instance;
	public static NPCSpawner Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<NPCSpawner>();
			}
			return instance;
		}
	}
	
	// Start is called before the first frame update
	void Start()
	{
		NpcPoolManager.Instance.CreatePool(civilian, civilianPoolSize);
		NpcPoolManager.Instance.CreatePool(guard, guardPoolSize);

		for (int i = 0; i < civilianPoolSize; i++)
		{
			NpcSpawn(true);
		}

		for (int i = 0; i < guardPoolSize; i++)
		{
			NpcSpawn(false);
		}
		//StartCoroutine(NpcSpawnEnumerator(true, 3f));

		
	}

	// Update is called once per frame
	void Update()
	{

		//All IF-statements below in Update are for testing purposes
		if (civiliansActive < civilianPoolSize)
		{
			//StartCoroutine(NpcSpawnEnumerator(true, 3f));
			NpcSpawn(true);
		}

		if (guardsActive < guardPoolSize)
		{
			NpcSpawn(false);
		}

		if (Input.GetKeyDown(KeyCode.G))
		{
			NpcDespawn(false, GameObject.FindGameObjectWithTag("Guard"));
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			NpcDespawn(true,GameObject.FindGameObjectWithTag("Civilian"));
		}
	}

	IEnumerator NpcSpawnEnumerator(bool isCivilian, float delayTime)
	{
		Debug.Log("Spawn Enum going");
		yield return new WaitForSeconds(delayTime);
		int i = Random.Range(0, spawnLocations.Length);
		Transform currentSpawn = spawnLocations[i];
		if (isCivilian && civiliansActive < civilianPoolSize)
		{
			NpcPoolManager.Instance.ReuseNpc(civilian, currentSpawn);
			civiliansActive++;
		}
		else if (guardsActive < guardPoolSize)
		{
			NpcPoolManager.Instance.ReuseNpc(guard, currentSpawn);
			guardsActive++;
		}
	}


	private void NpcSpawn(bool isCivilian)
	{
		int i = Random.Range(0, spawnLocations.Length);
		Transform currentSpawn = spawnLocations[i];
		if (isCivilian)
		{
			NpcPoolManager.Instance.ReuseNpc(civilian, currentSpawn);
			civiliansActive++;
		}
		else
		{
			NpcPoolManager.Instance.ReuseNpc(guard, currentSpawn);
			guardsActive++;
		}
	}

	public void NpcDespawn(bool isCivilian, GameObject npc)
	{
		npc.SetActive(false);

		if (isCivilian)
		{
			civiliansActive--;
		}
		else
		{
			guardsActive--;
		}
	}
}