using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuDraft : MonoBehaviour
{
	[SerializeField] GameObject howToPlay;

	private void Start()
	{
		howToPlay.SetActive(false);
	}

	public void Play()
	{
		SceneManager.LoadScene(1);
	}

	public void ToggleInfo()
	{
		howToPlay.SetActive(!howToPlay.activeSelf);
	}
}
