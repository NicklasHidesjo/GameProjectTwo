using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSettings : MonoBehaviour
{
    public Slider sence;
    private void Start()
    {
        sence.value = PlayerPrefs.GetFloat("CameraSensitivity", sence.maxValue * 0.5f);
    }

    public void CameraSensitivity(float value)
    {
        PlayerPrefs.SetFloat("CameraSensitivity", value);
    }
}
