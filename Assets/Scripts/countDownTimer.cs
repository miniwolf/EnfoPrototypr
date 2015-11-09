using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class countDownTimer : MonoBehaviour {
    public float timeRemaining = 60;
    public Text waveText;
    private int waveCount = 1;
    private bool isPause = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeRemaining -= Time.deltaTime;

        switch(isPause) 
        {
            case true:
                if(timeRemaining > 0)
                    waveText.text = "Next wave in " + timeRemaining.ToString("0");
                else
                {
                    isPause = false;
                    timeRemaining = 60;
                }
                break;
            case false:
                if(timeRemaining > 0)
                    waveText.text = "Wave " + waveCount + ": " + timeRemaining.ToString("0");
                else
                {
                    isPause = true;
                    waveCount++;
                    timeRemaining = 30;
                }
                break;
        }
	}
}
