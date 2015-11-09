using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class countDownTimer : MonoBehaviour {
    public float timeRemaining = 60;
    public Text waveText;
    private int waveCount = 1;
    public static bool isSpawning = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeRemaining -= Time.deltaTime;

        switch(isSpawning) 
        {
            case true:
                if(timeRemaining > 0)
                    waveText.text = "Next wave in " + timeRemaining.ToString("0");
                else
                {
                    isSpawning = false;
                    timeRemaining = 60;
                }
                break;
            case false:
                if(timeRemaining > 0)
                    waveText.text = "Wave " + waveCount + ": " + timeRemaining.ToString("0");
                else
                {
                    isSpawning = true;
                    waveCount++;
                    timeRemaining = 30;
                }
                break;
        }
	}
}
