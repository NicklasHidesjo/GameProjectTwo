using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNotoriousLevels : MonoBehaviour
{
    private float plWantedLevel = 1;
    private float plSuspiusLevel = 1;
    private float plLuminosity = 1;
    
    public float GetPlayerNotoriousLevel()
    {
        return ((plWantedLevel + plSuspiusLevel) * 0.5f * plLuminosity);
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
}
