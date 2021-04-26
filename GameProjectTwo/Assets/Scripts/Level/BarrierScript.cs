using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    int ActiveLevel;
    [SerializeField] GameObject[] BarrierLevels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            LevelChange();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetAll();
        }
    }
    
    void LevelChange()
    {
        BarrierLevels[ActiveLevel].SetActive(false);
        ActiveLevel++;
    }

    void ResetAll()
    {
        for (int i = 0; i < BarrierLevels.Length; i++)
        {
            BarrierLevels[i].SetActive(true);
        }
        ActiveLevel = 0;
    }

  /* ToDo if needed  void Loadlevel(int levelInput)
    {

    } */
}
