using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : LevelManager {
	public Text waveText;
	public static bool isSpawning = true;
	private bool gameIsWon;
	private int waveSpawningTime = 10; // TEST values
	private int waveWaitingTime = 2;
	public int maxWaves = 4;
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

		switch (isSpawning) {
			case true:
				if(waveTime > 0) {
					waveText.text = "Wave " + waveCount + " - " + waveTime.ToString("0") + " s";
				}
				else {
					isSpawning = false;
					waveTime = waveWaitingTime;
				}
				break;
			case false:
				if (waveTime > 0) {
					waveText.text = "Next wave " + waveTime.ToString("0") + " s";
				}
				else {
					waveCount++;
					isSpawning = true;
					waveTime = waveSpawningTime;
					if (waveCount >= maxWaves) {
						waveText.text = "FINAL WAVE";
						enabled = false; // Disables the Update() function
						break;
					}
				}
				break;
		}
	}
}
