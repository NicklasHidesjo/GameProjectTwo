using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPCSpawner : MonoBehaviour
{
	[SerializeField] NPC civilian;
	[SerializeField] NPC guard;
	[SerializeField] SpawnPath[] civilianSpawnPos;
	[SerializeField] SpawnPath[] guardSpawnPos;


	[SerializeField] private int civilianPoolSize = 5;
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
		InstantiateNPCs();
		InitializeNPCs();
	}

	private void InitializeNPCs()
	{
		foreach (var npc in inactiveCivs)
		{

			int i = Random.Range(0, civilianSpawnPos.Length);
			Transform currentSpawn = civilianSpawnPos[i].transform;

			activeCivs.Add(npc);

			List<PathPoint> path = civilianSpawnPos[Random.Range(0, civilianSpawnPos.Length)].GetPath().ToList();

			npc.transform.position = currentSpawn.position;
			npc.gameObject.SetActive(true);
			npc.InitializeNPC(path);
		}
		inactiveCivs.Clear();

		foreach (var npc in inactiveGuards)
		{
			/*			int i = Random.Range(0, guardSpawnPos.Length);
						Transform currentSpawn = guardSpawnPos[i].transform;

						activeGuards.Add(npc);

						SpawnPath spawnPath = guardSpawnPos[Random.Range(0, guardSpawnPos.Length)];

						List<PathPoint> path = spawnPath.GetRandomizedPatrolPath().ToList();

						npc.transform.position = currentSpawn.position;
						npc.gameObject.SetActive(true);
						npc.InitializeNPC(path, spawnPath.BackTrack);
			*/

			npc.transform.position = npc.startingPath.transform.position;
			npc.gameObject.SetActive(true);
			npc.InitializeNPC(npc.startingPath.GetPath(), npc.startingPath.BackTrack);
		}
		inactiveGuards.Clear();

		respawnTimerCivilian = 0f;
		respawnTimerGuard = 0f;
	}

	private void InstantiateNPCs()
	{
		for (int i = 0; i < civilianPoolSize; i++)
		{
			SpawnNPCs(true);
		}

		foreach (var spawnPoint in guardSpawnPos)
		{
			for (int i = 0; i < spawnPoint.NumOfGuards; i++)
			{
				SpawnNPCs(false, spawnPoint);
			}
		}
	}
	private void SpawnNPCs(bool isCivilian, SpawnPath startingPath = null)
	{
		NPC npc;
		if (isCivilian)
		{
			npc = Instantiate(civilian, transform);
			inactiveCivs.Add(npc);
		}
		else
		{
			npc = Instantiate(guard, transform);
			npc.startingPath = startingPath;
			inactiveGuards.Add(npc);
		}
		npc.gameObject.SetActive(false);
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
#if UNITY_EDITOR
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
			print("SpawnNPCs(true);");
		}
		if(Input.GetKeyDown(KeyCode.G))
		{
			SpawnNPCs(false);
		}
#endif

	}
	
	private void NpcSpawn(bool isCivilian)
	{
		List<PathPoint> path;
		Transform currentSpawn;
		bool backTrack = false;

		NPC npc;
		if (isCivilian)
		{
			int i = Random.Range(0, civilianSpawnPos.Length);
			currentSpawn = civilianSpawnPos[i].transform;

			if (inactiveCivs.Count < 1) 
			{ 
				return; 
			}
			path = civilianSpawnPos[Random.Range(0,civilianSpawnPos.Length)].GetPath().ToList();
			npc = inactiveCivs[0];
			inactiveCivs.Remove(npc);
			activeCivs.Add(npc);
		}
		else
		{
			int i = Random.Range(0, guardSpawnPos.Length);
			currentSpawn = guardSpawnPos[i].transform;

			if (inactiveGuards.Count < 1) 
			{ 
				return; 
			}
			SpawnPath spawnPath = guardSpawnPos[Random.Range(0, guardSpawnPos.Length)];

			path = spawnPath.GetPath().ToList();

			npc = inactiveGuards[0];
			inactiveGuards.Remove(npc);
			activeGuards.Add(npc);
			backTrack = spawnPath.BackTrack;

		}
		npc.transform.position = currentSpawn.position;
		npc.gameObject.SetActive(true);
		npc.InitializeNPC(path,backTrack);
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

	public void ResetNPCs()
	{
		foreach (var npc in activeCivs)
		{
			inactiveCivs.Add(npc);
		}
		activeCivs.Clear();

		foreach (var npc in activeGuards)
		{
			inactiveGuards.Add(npc);
		}
		activeGuards.Clear();

		InitializeNPCs();
	}
}