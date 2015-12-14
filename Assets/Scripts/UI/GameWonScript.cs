using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameWonScript : MonoBehaviour {
	public Canvas gameWonMenu;
	public Button restartButton;

	// Use this for initialization
	void Start()
	{
		gameWonMenu = gameWonMenu.GetComponent<Canvas>();
		restartButton = restartButton.GetComponent<Button>();
		restartButton.enabled = true;
	}

	public void RestartGame()
	{
		WaveManager.ResetWaves();
		SceneManager.LoadScene("mergeScene");
	}
}
