using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    private int activeLevel;
    [SerializeField] GameObject[] barrierLevels;
    void Start()
    {
        EndLevelCheck.OnLevelEnded += LevelChange;
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
    
    void LevelChange(int newLevel)
    {
        barrierLevels[activeLevel].SetActive(false);
        activeLevel = newLevel;
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
        EndLevelCheck.OnLevelEnded -= LevelChange;

    }
}
