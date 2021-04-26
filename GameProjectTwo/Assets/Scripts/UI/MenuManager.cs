using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("End of level Screen")]
    [SerializeField] private GameObject endOfLevelScreen;
    [SerializeField] private GameObject pauseScreen;

    private bool gamePaused = false;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                pauseScreen.SetActive(false);
                TogglePause();
            }
            else
            {
                pauseScreen.SetActive(true); 
                TogglePause();                
            }
        }
    }

    public void RestartGame()
    {
        TogglePause();
        SceneManager.LoadScene("210422", LoadSceneMode.Single);
    }

    public void TogglePause()
    {
        if (gamePaused)
        {
            Cursor.visible = false;
            Time.timeScale = 1f;
            gamePaused = false;
        }
        else
        {
            Cursor.visible = true;
            Time.timeScale = 0f;
            gamePaused = true;
        }
    }

    public void ShowEndOfLevelScreen()
    {
        endOfLevelScreen.SetActive(true);
        TogglePause();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
