using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNotoriousLevels : MonoBehaviour
{
    public Material debugMat;

    private float plWantedLevel = 1;
    private float plSuspiusLevel = 1;
    private float plLuminosity = 0;
    private float baseSuspision = 0;

    [SerializeField] int maxNumberOfSloppyKills = 10;
    [SerializeField] int numberOfSloppyKills;
    public float GetPlayerNotoriousLevel()
    {
        
        return ((plWantedLevel + plSuspiusLevel * plLuminosity) / 2);
    }

    public void SetPlWantedLevel(float level)
    {
        plWantedLevel = level;
    }

    public void SetPlSuspiusLevel(float level)
    {
        plSuspiusLevel = level;
    }

    public void SetPlLuminosity(float level)
    {
        plLuminosity = level;
    }

    public void AddSeenDeadBody()
    {
        numberOfSloppyKills++;
        plWantedLevel = (float)numberOfSloppyKills / (float)maxNumberOfSloppyKills;
    }
}
