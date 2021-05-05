using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundTest : MonoBehaviour
{

    public SoundType soundToTest;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.I))
        {
            AudioManager.instance.PlaySound(soundToTest, gameObject); 
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            AudioManager.instance.StopSound(gameObject, 1f);
        }
#endif
    }



}
