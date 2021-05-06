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
	[SerializeField] private float respawnRate = 5f;

	private float respawnTimerCivilian;
	private float respawnTimerGuard;

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

		respawnTimerCivilian = 0f;
		respawnTimerGuard = 0f;
	}
	
	void Update()
	{
		//Making sure there are enough civilians in scene.
		if (civiliansActive < civilianPoolSize)
		{
			respawnTimerCivilian += Time.deltaTime;

			if (respawnTimerCivilian >= respawnRate)
			{
				NpcSpawn(true);
				respawnTimerCivilian = 0f;
			}
		}

		//Making sure there are enough guards in scene.
		if (guardsActive < guardPoolSize)
		{
			respawnTimerGuard += Time.deltaTime;

			if (respawnTimerGuard >= respawnRate)
			{
				NpcSpawn(false);
				respawnTimerGuard = 0f;
			}
		}

		//All IF-statements below in Update are for testing purposes

		if (Input.GetKeyDown(KeyCode.G))
		{
			NpcDespawn(false, GameObject.FindGameObjectWithTag("Guard"));
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			NpcDespawn(true,GameObject.FindGameObjectWithTag("Civilian"));
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