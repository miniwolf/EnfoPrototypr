using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public Canvas gameOverMenu;
	public Button restartButton;


	// Use this for initialization
	void Start () {
		gameOverMenu = gameOverMenu.GetComponent<Canvas>();
		restartButton = restartButton.GetComponent<Button>();
		restartButton.enabled = true;
	}
	
	public void RestartGame()
	{
		// Resettings parameters for new game
		Application.LoadLevel(0);
		TargetManager.targetLife = 30;
		WaveManager.timeRemaining = 60;
		WaveManager.waveCount = 1;
		WaveManager.isSpawning = true;
		navScript.agentSpeed = 5;
		navScript.agentAccel = 5;
		EnemyManager.spawnTime = 5f;
	}
}
