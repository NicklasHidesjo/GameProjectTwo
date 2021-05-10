using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNotoriousLevels : MonoBehaviour
{
    public Material debugMat;

    private float plLongSuspiciousLevel = 0;
    private float plShortSuspiciousLevel = 0;
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

    public float GetPlayerNotoriousLerpLevel()
    {
        if (PlayerManager.instance.PlayerState.CurrentState == PlayerState.playerStates.BatDefault)
            return 0;

        if (PlayerManager.instance.PlayerState.CurrentState == PlayerState.playerStates.DraculaHidden)
            return 0;

        print("NLevel : " + (plLongSuspiciousLevel + plShortSuspiciousLevel + plLuminosity) / 3);
        return (plLongSuspiciousLevel + plShortSuspiciousLevel + plLuminosity) / 3;
    }

    public float GetPlayerNotoriousLevel()
    {
        //1-2
        float nLevel = (plLongSuspiciousLevel + plShortSuspiciousLevel) / 2;
        nLevel += 1;
        nLevel *= plLuminosity;
        print("NLevel : "+ nLevel);
        return nLevel;
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

    public void AddPlShortSuspiciousLevel(float level)
    {
        plShortSuspiciousLevel += level;
        plShortSuspiciousLevel = Mathf.Clamp01(plShortSuspiciousLevel);
    }  
    public void AddPlLongtSuspiciousLevel(float level)
    {
        plLongSuspiciousLevel += level / maxNumberOfSloppyKills;
        plLongSuspiciousLevel = Mathf.Clamp01(plLongSuspiciousLevel);
    }

    public void AddPlLuminosity(float level)
    {
        plLuminosity += level / maxNumberOfSloppyKills;
    }

    public void AddSeenDeadBody()
    {
        numberOfSloppyKills++;
        plShortSuspiciousLevel++;
        plLongSuspiciousLevel = (float)numberOfSloppyKills / (float)maxNumberOfSloppyKills;
    }
}
