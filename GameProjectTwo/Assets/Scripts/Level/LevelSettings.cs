using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] float timeTillSunrise;
    SunTimeOfDay sunTime;

    // Start is called before the first frame update
    void Start()
    {
        //Find Sun
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light l in lights)
        {
            if (l.GetComponent<SunTimeOfDay>())
            {
                sunTime = l.GetComponent<SunTimeOfDay>();
            }
        }
        

        LevelStart();
    }

    void LevelStart()
    {
        sunTime.SetTimeOfDayTo(0);
        sunTime.SetRiseTimer(timeTillSunrise, 10, 3);
    }
}
