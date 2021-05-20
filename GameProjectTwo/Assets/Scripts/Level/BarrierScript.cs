using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    private int activeLevel;
    [SerializeField] GameObject[] barrierLevels;
    void Start()
    {
        MenuManager.OnLevelStart += LevelChange;

        foreach(GameObject o in barrierLevels)
        {
            o.SetActive(false);
        }

        barrierLevels[0].SetActive(true);
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.C))
        //{
        //    LevelChange();
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    ResetAll();
        //}
    }
    
    void LevelChange()
    {
        barrierLevels[activeLevel].SetActive(false);
        activeLevel++;
        barrierLevels[activeLevel].SetActive(true);
    }

    void ResetAll()
    {
        for (int i = 0; i < barrierLevels.Length; i++)
        {
            barrierLevels[i].SetActive(true);
        }
        activeLevel = 0;
    }

    /* ToDo if needed  void Loadlevel(int levelInput)
      {

      } */

    private void OnDestroy()
    {
        MenuManager.OnLevelStart -= LevelChange;

    }
}
