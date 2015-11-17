﻿using UnityEngine;
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
        Application.LoadLevel(0);
    }
}
