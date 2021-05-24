using UnityEngine;
using TMPro;
public class MainMenuManager : MonoBehaviour
{
	[SerializeField] GameObject baseMenuPanel;
	[SerializeField] GameObject howToPlayPanel;
	[SerializeField] GameObject optionsPanel;
	[SerializeField] AudioOptions audioOptions;

	[SerializeField] GameObject[] HowToPlayTexts;
	[SerializeField] TextMeshProUGUI IndexNumber;
	[SerializeField] TextMeshProUGUI previousPageTitle;
	[SerializeField] TextMeshProUGUI nextPageTitle;
	[SerializeField] TextMeshProUGUI currentPageTitle;

	int currentPageIndex;


	private void Start()
	{
		baseMenuPanel.SetActive(true);
		howToPlayPanel.SetActive(false);
		optionsPanel.SetActive(false);
		audioOptions = GetComponent<AudioOptions>();
	}

	public void ToggleHowToPlay()
	{
		howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
		currentPageIndex = 0;
		UpdatePageTexts();
	}

	public void IncreasePageIndex()
	{
		currentPageIndex++;
		if(currentPageIndex >= HowToPlayTexts.Length)
		{
			currentPageIndex = 0;
		}
		UpdatePageTexts();
	}
	public void DecreasePageIndex()
	{
		currentPageIndex--;
		if (currentPageIndex < 0)
		{
			currentPageIndex = HowToPlayTexts.Length - 1;
		}
		UpdatePageTexts();
	}
	public void UpdatePageTexts()
	{
		foreach (var page in HowToPlayTexts)
		{
			page.SetActive(false);
		}

		HowToPlayTexts[currentPageIndex].SetActive(true);
		IndexNumber.text = (currentPageIndex + 1) + "/" + (HowToPlayTexts.Length);
		/*
		currentPageTitle.text = HowToPlayTexts[currentPageIndex].name;
		
		int nextPage = currentPageIndex + 1;
		if (nextPage > HowToPlayTexts.Length -1)
		{
			nextPage = 0;
		}
		int previousPage = currentPageIndex -1;
		if (previousPage < 0)
		{
			previousPage = HowToPlayTexts.Length - 1;
		}

		previousPageTitle.text = HowToPlayTexts[previousPage].name;
		nextPageTitle.text = HowToPlayTexts[nextPage].name;
		*/
	}


	public void ToggleOptions()
	{
		optionsPanel.SetActive(!optionsPanel.activeSelf);
		if (optionsPanel.activeSelf)
		{
			audioOptions.SetSliders();

		}
	}

	public void ToggleBaseMenu()
	{
		baseMenuPanel.SetActive(!baseMenuPanel.activeSelf);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
