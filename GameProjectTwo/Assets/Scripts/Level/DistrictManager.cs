using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour
{
    private int currentlyActiveCivilians;
    private int currentlyActiveGuards;
    
    [SerializeField] private int activeCiviliansNormal = 5;
    public int CurrentlyActiveCivilians => currentlyActiveCivilians;
    public int CurrentlyActiveGuards => currentlyActiveGuards;
    public int ActiveCiviliansNormal => activeCiviliansNormal;
    
    [SerializeField] private Transform[] civilianSpawnPoints;
    
    public Transform[] CivilianSpawnPoints => civilianSpawnPoints;

    [SerializeField] private Transform[] guardSpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LocalCivilianSpawn()
    {
        NPCSpawner.Instance.NpcSpawn(true, civilianSpawnPoints);
        currentlyActiveCivilians++;
    }

    private void LocalCivilianDespawn()
    {
        
    }
}
