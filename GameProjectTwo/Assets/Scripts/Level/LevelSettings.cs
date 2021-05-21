using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] float timeTillSunrise;

    private SunTimeOfDay sunTime;


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
        MenuManager.OnLevelStart += LevelStart;
    }

    public void LevelStart()
    {
        print("Sun rise in " + timeTillSunrise + " seconds. Need To eat enough until then"); 
        sunTime.MoveTimeOfDayTo(0, 1);
        sunTime.SetRiseTimer(timeTillSunrise, 7, 5);
    }

    private void OnDestroy()
    {
        MenuManager.OnLevelStart -= LevelStart;
    }

}
