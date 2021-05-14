using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPCSpawner : MonoBehaviour
{
	[SerializeField] NPC civilian;
	[SerializeField] NPC stationaryCivilian;
	[SerializeField] NPC guard;
	[SerializeField] NPC stationaryGuard;
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
			List<PathPoint> path;
			Transform currentSpawn;

			if(npc.Stationary)
			{
				currentSpawn = npc.startingPath.SpawnPos;
				path = null;
			}
			else
			{
				int i = Random.Range(0, civilianSpawnPos.Length);
				currentSpawn = civilianSpawnPos[i].SpawnPos;
				path = civilianSpawnPos[i].GetPath().ToList();
			}

			activeCivs.Add(npc);

			npc.transform.position = currentSpawn.position;
			npc.gameObject.SetActive(true);
			npc.InitializeNPC(path);
		}
		inactiveCivs.Clear();

		foreach (var npc in inactiveGuards)
		{
			activeGuards.Add(npc);
			npc.transform.position = npc.startingPath.SpawnPos.position;
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
			CreateNPC(true);
		}
		foreach (var spawnPoint in civilianSpawnPos)
		{
			if (spawnPoint.Stationary)
			{
				CreateStationaryNPC(true, spawnPoint);
			}
		}

		foreach (var spawnPoint in guardSpawnPos)
		{
			if(spawnPoint.Stationary)
			{
				CreateStationaryNPC(false, spawnPoint);
				continue;
			}

			for (int i = 0; i < spawnPoint.NumOfGuards; i++)
			{
				CreateNPC(false, spawnPoint);
			}
		}
	}
	private void CreateNPC(bool isCivilian, SpawnPath startingPath = null)
	{
		NPC npc;
		if (isCivilian)
		{
			Transform currentSpawn = civilianSpawnPos[Random.Range(0,civilianSpawnPos.Length)].SpawnPos;
			npc = Instantiate(civilian, currentSpawn.position, Quaternion.identity, transform);
			inactiveCivs.Add(npc);
		}
		else
		{
			npc = Instantiate(guard, startingPath.SpawnPos.position, Quaternion.identity, transform);
			npc.startingPath = startingPath;
			inactiveGuards.Add(npc);
		}
		npc.gameObject.SetActive(false);
	}
	private void CreateStationaryNPC(bool isCivilian, SpawnPath startingPath)
	{
		NPC npc;
		if (isCivilian)
		{
			npc = Instantiate(stationaryCivilian, startingPath.SpawnPos.position, startingPath.SpawnPos.rotation, transform);
			npc.startingPath = startingPath;
			inactiveCivs.Add(npc);
		}
		else
		{
			npc = Instantiate(stationaryGuard, startingPath.SpawnPos.position, startingPath.SpawnPos.rotation, transform);
			npc.startingPath = startingPath;
			inactiveGuards.Add(npc);
		}
		npc.gameObject.SetActive(false);
	}

	private void Update()
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
			NpcDespawn(true, GameObject.FindGameObjectWithTag("Civilian").GetComponent<NPC>());
		}

		if (Input.GetKeyDown(KeyCode.U))
		{
			CreateNPC(true);
			print("SpawnNPCs(true);");
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			CreateNPC(false);
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
			if (inactiveCivs.Count < 1) 
			{ 
				return; 
			}

			npc = inactiveCivs[0];
			
			if (npc.Stationary)
			{
				currentSpawn = npc.startingPath.SpawnPos;
				path = null;
			}
			else
			{
				int i = Random.Range(0, civilianSpawnPos.Length);
				currentSpawn = civilianSpawnPos[i].SpawnPos;
				path = civilianSpawnPos[i].GetPath().ToList();
			}

			inactiveCivs.Remove(npc);
			activeCivs.Add(npc);
		}
		else
		{

			if (inactiveGuards.Count < 1) 
			{ 
				return; 
			}

			npc = inactiveGuards[0];

			path = npc.startingPath.GetPath();
			currentSpawn = npc.startingPath.SpawnPos;
			inactiveGuards.Remove(npc);
			activeGuards.Add(npc);
			backTrack = npc.startingPath.BackTrack;
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

	public void RemoveStationaryNPC(NPC npc)
	{
		npc.gameObject.SetActive(false);
		activeCivs.Remove(npc);
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