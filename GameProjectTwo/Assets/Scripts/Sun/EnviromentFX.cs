using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentFX : MonoBehaviour
{
    bool lightSwitch = false;

    [SerializeField] Color fogNightCol;
    [SerializeField] Color fogDayCol;
    [SerializeField] float fogDistNight = 10;
    [SerializeField] float fogDistDay = 20;
    [SerializeField] float dotMul = 4;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogEndDistance = fogDistNight;
        RenderSettings.fogColor = fogNightCol;
        lightSwitch = false;
    }

    // Update is called once per frame
    public void UpdateEnviroment(float sunValue)
    {
        RenderSettings.fogEndDistance = Mathf.Lerp(fogDistNight, fogDistDay, sunValue);
        RenderSettings.fogColor = Color.Lerp(fogNightCol, fogDayCol, sunValue);
    }
}
