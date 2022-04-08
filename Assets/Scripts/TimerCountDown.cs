using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerCountDown : MonoBehaviour
{
	public TextMeshProUGUI textDisplay;
	public float gameTime;
	public float level2GameTime;
	private float timer;
	private bool stopTimer;


	private void Start()
	{
		stopTimer = false;
		timer = gameTime;
	}

	private void Update()
	{
		UpdateTimer();
	}

	private void UpdateTimer()
	{
		timer -= Time.deltaTime;
		int minutes = Mathf.FloorToInt(timer / 60);
		int seconds = Mathf.FloorToInt(timer - minutes * 60f);
		string textTime = string.Format("{0:00}:{1:00}", minutes, seconds);
		if (timer <= 0)
		{
			stopTimer = true;
			timer = 0;
		}
		if (!stopTimer)
		{
			textDisplay.SetText(textTime);
		}
	}
	
}
