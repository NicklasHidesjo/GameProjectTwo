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
    [SerializeField] private GameObject deathScreen;

    private bool inDeathScreen;

    private bool gamePaused = false;

    private void Start()
    {
        inDeathScreen = false;
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

        if (PlayerManager.instance.StatsManager.IsDead && !inDeathScreen)
        {
            PlayerDeathScreen();
        }
    }

    public void RestartGame()
    {
        TogglePause();
        //TODO Load correct scene
        String currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
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

    private void PlayerDeathScreen()
    {
        inDeathScreen = true;
        deathScreen.SetActive(true);
        TogglePause();
    }

    public void EndOfLevelScreen()
    {
        endOfLevelScreen.SetActive(true);
        TogglePause();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
