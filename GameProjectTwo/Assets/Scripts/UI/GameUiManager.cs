using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUiManager : MonoBehaviour
{
	public static GameUiManager instance;

	[SerializeField] Image SunIndicator;
	private Coroutine fadeIn;
    private Coroutine fadeOut;

    private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
			Destroy(gameObject);
	}

    public void SetSunIndicatorAlpha(float amount, float max)
    {
		max = max == 0 ? 0.001f : max;
		Color c = SunIndicator.color;
		c.a = (amount / max) * 0.5f;
		SunIndicator.color = c;
	}


	IEnumerator FadeInUiImage(Image UiImage)
	{
		
	
		float alpha = UiImage.color.a;

		for (float ft = alpha; ft <= 0.5f; ft += 0.01f)
		{
			Color c = UiImage.color;
			c.a = Mathf.Lerp(alpha, 0.5f, ft);
			UiImage.color = c;

			yield return null;

		}
	}

	IEnumerator FadeOutUiImage(Image UiImage)
	{
		
		float alpha = UiImage.color.a;

		for (float ft = alpha; ft >= 0; ft -= 0.01f)
		{
			Color c = UiImage.color;
			c.a = Mathf.Lerp(0, alpha, ft);
			UiImage.color = c;

			yield return null;

		}

	}

}
