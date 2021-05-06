using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
	[SerializeField] NPC civilian;
	[SerializeField] NPC guard;
	[SerializeField] Transform[] spawnLocations;

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


	List<NPC> activeCivs = new List<NPC>();
	List<NPC> inactiveCivs = new List<NPC>();	
	
	List<NPC> activeGuards= new List<NPC>();
	List<NPC> inactiveGuards = new List<NPC>();



	void Start()
	{
		/*		NpcPoolManager.Instance.CreatePool(civilian, civilianPoolSize);
				NpcPoolManager.Instance.CreatePool(guard, guardPoolSize);*/

		for (int i = 0; i < civilianPoolSize; i++)
		{
			SpawnNPCs(true);
		}

		for (int i = 0; i < guardPoolSize; i++)
		{
			SpawnNPCs(false);
		}

		foreach (var npc in inactiveCivs)
		{
			int i = Random.Range(0, spawnLocations.Length);
			Transform currentSpawn = spawnLocations[i];

			activeCivs.Add(npc);

			npc.transform.position = currentSpawn.position;
			npc.gameObject.SetActive(true);
			npc.InitializeNPC();
		}
		inactiveCivs.Clear();

		foreach (var npc in inactiveGuards)
		{
			int i = Random.Range(0, spawnLocations.Length);
			Transform currentSpawn = spawnLocations[i];

			activeGuards.Add(npc);

			npc.transform.position = currentSpawn.position;
			npc.gameObject.SetActive(true);
			npc.InitializeNPC();
		}
		inactiveGuards.RemoveAll(npc => npc == null);

		respawnTimerCivilian = 0f;
		respawnTimerGuard = 0f;
	}
	
	void Update()
	{
		//Making sure there are enough civilians in scene.
		if (inactiveCivs.Count > 0)
		{
			respawnTimerCivilian += Time.deltaTime;

			if (respawnTimerCivilian >= respawnRate)
			{
				NpcSpawn(true);
				respawnTimerCivilian = 0f;
				inactiveCivs.RemoveAll(npc => npc == null);
			}
		}

		//Making sure there are enough guards in scene.
		if (inactiveGuards.Count > 0)
		{
			respawnTimerGuard += Time.deltaTime;

			if (respawnTimerGuard >= respawnRate)
			{
				NpcSpawn(false);
				respawnTimerGuard = 0f;
				inactiveGuards.RemoveAll(npc => npc == null);
			}
		}

		//All IF-statements below in Update are for testing purposes

		if (Input.GetKeyDown(KeyCode.V))
		{
			NpcDespawn(false, GameObject.FindGameObjectWithTag("Guard").GetComponent<NPC>());
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			NpcDespawn(true,GameObject.FindGameObjectWithTag("Civilian").GetComponent<NPC>());
		}

		if(Input.GetKeyDown(KeyCode.U))
		{
			SpawnNPCs(true);
		}
		if(Input.GetKeyDown(KeyCode.G))
		{
			SpawnNPCs(false);
		}
	}
	
	private void NpcSpawn(bool isCivilian)
	{
		int i = Random.Range(0, spawnLocations.Length);
		Transform currentSpawn = spawnLocations[i];
		NPC npc;
		if (isCivilian)
		{
			if(inactiveCivs.Count < 1) 
			{ 
				return; 
			}

			npc = inactiveCivs[0];
			inactiveCivs.Remove(npc);
			activeCivs.Add(npc);
			npc.InitializeNPC();
		}
		else
		{
		
			if(inactiveGuards.Count < 1) 
			{ 
				return; 
			}
			
			npc = inactiveGuards[0];
			inactiveGuards.Remove(npc);
			activeGuards.Add(npc);


		}
		npc.transform.position = currentSpawn.position;
		npc.gameObject.SetActive(true);
		npc.InitializeNPC();
	}

	private void SpawnNPCs(bool isCivilian)
	{
		int i = Random.Range(0, spawnLocations.Length);
		Transform currentSpawn = spawnLocations[i];
		NPC npc;
		if (isCivilian)
		{
			npc = Instantiate(civilian, currentSpawn.position, Quaternion.identity, transform);
			inactiveCivs.Add(npc);
		}
		else
		{
			npc = Instantiate(guard, currentSpawn.position, Quaternion.identity, transform);
			inactiveGuards.Add(npc);
		}
		npc.gameObject.SetActive(false);
	}

	public void NpcDespawn(bool isCivilian, NPC npc)
	{
		npc.gameObject.SetActive(false);
		if (isCivilian)
		{
			activeCivs.Remove(npc);
			inactiveCivs.Add(npc);
			activeCivs.RemoveAll(npc => npc == null);
		}
		else
		{
			activeGuards.Remove(npc);
			inactiveGuards.Add(npc);
			activeGuards.RemoveAll(npc => npc == null);
		}
	}
}