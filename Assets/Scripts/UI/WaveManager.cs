using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : MonoBehaviour {
	private Text waveText;
	private bool gameIsWon;
	private int waveWaitingTime = 20;
	private float waveTime = waveSpawningTime;

	private static int waveSpawningTime = 30;
	private static int maxWaves = 4;
	private static bool isSpawning = true;
	private static int waveCount = 1;

	public static int MaxWaves {
		get {
			return maxWaves;
		}
	}

	public static bool IsSpawning {
		get {
			return isSpawning;
		}
	}

	public static int WaveCount {
		get {
			return waveCount;
		}
	}

	public static void ResetWaves() {
		waveCount = 1;
		isSpawning = true;
	}

	void Start() {
		waveText = GetComponentInChildren<Text>();
	}

	// Update is called once per frame
	void Update () {
		waveTime -= Time.deltaTime;
		if ( waveTime > 0 ) {
			waveText.text = (isSpawning ? "Wave " + waveCount + " - " : "Next wave ") +
							waveTime.ToString("0") + " s";
		} else {
			waveTime = isSpawning ? waveWaitingTime : waveSpawningTime;
			if ( !isSpawning ) {
				if ( ++waveCount >= maxWaves) {
					waveText.text = "FINAL WAVE";
					isSpawning = true; // To allow last round boss spawn
					enabled = false; // Disables the Update() function
					return;
				}
			}
			isSpawning = !isSpawning;
		}
	}
}
