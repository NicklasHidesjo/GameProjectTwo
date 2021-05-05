using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour
{
    private int currentlyActiveCivilians;
    private int currentlyActiveGuards;
    
    [SerializeField] private int activeCiviliansNormal = 5;

    [SerializeField] private Transform[] civilianSpawnPoints;
    
    public Transform[] CivilianSpawnPoints => civilianSpawnPoints;

    [SerializeField] private Transform[] guardSpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        currentlyActiveCivilians = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LocalCivilianDespawn();
        }

        if (currentlyActiveCivilians < activeCiviliansNormal)
        {
            LocalCivilianSpawn();
        }
        
    }

    private void LocalCivilianSpawn()
    {
        NPCSpawner.Instance.NpcSpawn(true, civilianSpawnPoints);
        currentlyActiveCivilians++;
    }

    private void LocalCivilianDespawn()
    {
        NPCSpawner.Instance.NpcDespawn(GameObject.FindWithTag("Civilian"), this.gameObject);
        currentlyActiveCivilians--;
    }
}
