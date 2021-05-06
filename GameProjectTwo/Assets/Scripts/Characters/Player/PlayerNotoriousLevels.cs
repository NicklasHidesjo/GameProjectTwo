using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNotoriousLevels : MonoBehaviour
{
    public Material debugMat;

    private float plLongSuspiciousLevel = 0;
    private float plShortSuspiciousLevel =10;
    private float plLuminosity = 0;

    [SerializeField] int maxNumberOfSloppyKills = 10;
    [SerializeField] int numberOfSloppyKills;

    // Start is called before the first frame update
    void Start()
    {
        EndLevelCheck.OnLevelEnded += OnNewLevel;
    }

    public void OnNewLevel(int layerIndex)
    {
        SetPlShortSuspiciousLevel(0);
    }

    public float GetPlayerNotoriousLevel()
    {
        print("NLevel : " + (plLongSuspiciousLevel + plShortSuspiciousLevel + plLuminosity) / 3);
        return (plLongSuspiciousLevel + plShortSuspiciousLevel + plLuminosity)/3;
    }

    public void SetPlLongSuspiciousLevel(float level)
    {
        plLongSuspiciousLevel = level;
    }

    public void SetPlShortSuspiciousLevel(float level)
    {
        plShortSuspiciousLevel = level;
    }

    public void SetPlLuminosity(float level)
    {
        plLuminosity = level;
    }

    public void AddSeenDeadBody()
    {
        numberOfSloppyKills++;
        plShortSuspiciousLevel++;
        plLongSuspiciousLevel = (float)numberOfSloppyKills / (float)maxNumberOfSloppyKills;
    }
}
