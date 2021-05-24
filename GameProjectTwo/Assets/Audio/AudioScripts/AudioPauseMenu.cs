using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPauseMenu : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider masterSlider;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1);
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume", 1);
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 1);
    }
}
