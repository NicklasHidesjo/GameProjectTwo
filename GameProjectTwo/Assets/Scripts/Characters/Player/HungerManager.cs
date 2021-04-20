using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HungerManager : MonoBehaviour
{
    private PlayerStatsManager playerStatsManager;

    private void Start()
    {
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    //Incase of need.
    /*public void LoseSatiation(int satiation)
    {
        playerStatsManager.DecreaseSatiationValue(satiation);
    }*/

    public void GainSatiation(int satiation)
    {
        playerStatsManager.IncreaseSatiationValue(satiation);
    }
}
