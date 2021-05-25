using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    private int activeLevel;
    [SerializeField] GameObject tutorialBarrier;
    void Start()
    {
        MenuManager.OnLevelStart += LevelChange;
        tutorialBarrier.SetActive(true);
    }
    
    void LevelChange()
    {
        tutorialBarrier.SetActive(false);
    }

    void ResetAll()
    {
        tutorialBarrier.SetActive(true);
    }

    private void OnDestroy()
    {
        MenuManager.OnLevelStart -= LevelChange;
    }
}
