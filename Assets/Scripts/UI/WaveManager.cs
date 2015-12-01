using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : LevelManager {
	public Text waveText;
	private bool isSpawning = true;

	private int waveSpawningTime = 60; // TEST values
	private int waveWaitTime = 30;

	public GameObject enemy;


	void Start()
	{
		waveCount = 1;
		waveTime = waveSpawningTime;
	}

	// Update is called once per frame
	void Update ()
	{
		waveTime -= Time.deltaTime;

		switch (isSpawning)
		{
			case true:
				if(waveTime > 0)
				{
					waveText.text = "Wave " + waveCount + " - " + waveTime.ToString("0");
				}
				else
				{
					isSpawning = false;
					waveTime = waveWaitTime;
				}
				break;
			case false:
				if (waveTime > 0)
				{
					waveText.text = "Next wave in " + waveTime.ToString("0");
				}
				else
				{
					isSpawning = true;
					waveTime = waveSpawningTime;
					waveCount++;
				}
				break;
		}
	}
}
