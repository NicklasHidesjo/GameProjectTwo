using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNotoriousLevels : MonoBehaviour
{
    private float plWantedLevel = 1;
    private float plSuspiusLevel = 1;
    private float plLuminosity = 1;

    [SerializeField] int maxNumberOfSloppyKills;
    [SerializeField] int numberOfSloppyKills;
    public float GetPlayerNotoriousLevel()
    {
        float sloppyKillLevel = (float)numberOfSloppyKills /(float)maxNumberOfSloppyKills;


        return ((plWantedLevel + plSuspiusLevel) * 0.25f + plLuminosity *0.5f);
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
    }
}
