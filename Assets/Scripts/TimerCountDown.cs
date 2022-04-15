using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerCountDown : MonoBehaviour
{
	public TextMeshProUGUI textDisplay;
	public float gameTime;
	public float level2GameTime;
	public float timer;
	private bool stopTimer;

	private AudioSource audioSource;
	public AudioSource timeOut;
	private void Start()
	{
		InitializeGame();
		stopTimer = false;
		timer = gameTime;
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if(!TileManager.Instance.GameEnded)
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
			if (!timeOut.isPlaying)
				timeOut.Play();
			TileManager.Instance.GameEnded = true;

			timer = 0;
		}
		if (!stopTimer)
		{
			textDisplay.SetText(textTime);
		}
	}
	public void DeductTime(float sec)
	{
		timer -= sec;
		if (!audioSource.isPlaying)
			audioSource.Play();
	}

	public void InitializeGame()
	{
		switch (Data.skillLevel)
		{
			case 0:
				gameTime = 35;
				break;
			case 1:
				gameTime = 55;
				break;
			case 2:
				gameTime = 75;
				break;

		}
	}
}
