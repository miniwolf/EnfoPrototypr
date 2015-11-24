using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class countDownTimer : MonoBehaviour {
    public static float timeRemaining = 60;
    public Text waveText;
    public static int waveCount = 1;
    public static bool isSpawning = true;

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
                {
                    waveText.text = "Wave " + waveCount + " - " + timeRemaining.ToString("0");
                }
                else
                {
                    isSpawning = false;
                    timeRemaining = 30;
                }
                break;
            case false:
                if(timeRemaining > 0)
                {
                    waveText.text = "Next wave in " + timeRemaining.ToString("0");
                }
                else
                {
                    isSpawning = true;
                    timeRemaining = 60;
                    waveCount++;
                }
                break;
        }
	}
}
