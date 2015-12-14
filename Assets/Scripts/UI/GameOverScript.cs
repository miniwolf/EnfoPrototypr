using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
		WaveManager.ResetWaves();
		SceneManager.LoadScene("mergeScene");
	}
}
