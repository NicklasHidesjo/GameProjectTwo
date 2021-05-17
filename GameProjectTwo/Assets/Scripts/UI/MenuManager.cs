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
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject levelChanger;

    
    private bool inDeathScreen;

    private bool gamePaused = false;

    public delegate void LevelStart();
    public static event LevelStart OnLevelStart;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
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

        if (player.IsDead)
        {
            PlayerDeathScreen();
        }
    }

    public void RestartGame()
    {
        TogglePause();
        //TODO Load correct scene
        AudioManager.instance.StopAll2DSounds();
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
    }

    public void ReturnToMainMenu()
    {
        AudioManager.instance.StopAll2DSounds();
        Time.timeScale = 1f;
		levelChanger.GetComponent<LevelChanger>().FadeToLevel(0);

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

    public void StartNextLevel()
    {
        if (OnLevelStart != null)
        {
            OnLevelStart();
        }
    }

    private void PlayerDeathScreen()
    {
        inDeathScreen = true;
        deathScreen.SetActive(true);
        AudioManager.instance.StopAll2DSounds();
        TogglePause();
    }

    public void EndOfLevelScreen()
    {
        
        endOfLevelScreen.SetActive(true);
        AudioManager.instance.StopAll2DSounds();

        TogglePause();
    }

    public void VictoryScreen()
    {
        victoryScreen.SetActive(true);
        TogglePause();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
