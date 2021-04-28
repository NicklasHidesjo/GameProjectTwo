using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour
{
    private int currentlyActiveCivilians;

    private int currentlyActiveGuards;

    public int CurrentlyActiveCivilians => currentlyActiveCivilians;

    public int CurrentlyActiveGuards => currentlyActiveGuards;

    [SerializeField] private Transform[] civilianSpawnPoints;
    
    public Transform[] CivilianSpawnPoints => civilianSpawnPoints;

    [SerializeField] private Transform[] guardSpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform transform in civilianSpawnPoints)
        {
            Debug.Log(transform.gameObject.name);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform[] GetCivilianSpawnPoints()
    {
        return civilianSpawnPoints;
    }
}
