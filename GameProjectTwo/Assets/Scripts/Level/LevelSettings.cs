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

        sunTime.SetTimeOfDayTo(0);
        LevelStart();
    }

    public void LevelStart()
    {
        sunTime.MoveTimeOfDayTo(0, 3);
        sunTime.SetRiseTimer(timeTillSunrise, 10, 20);
    }

}
