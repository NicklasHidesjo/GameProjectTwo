using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private LevelChanger levelChanger;

    public void BackToMainMenu()
    {
        levelChanger.FadeToLevel(0);
    }
}
