using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialImageScript : MonoBehaviour {

	public Canvas canvas;
	public Image tutorialImage;
	public Text tutorialText;
	public Button OKbutton;
	public Text OKtext;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0.0f;
		canvas = canvas.GetComponent<Canvas>();
		tutorialImage = tutorialImage.GetComponent<Image>();
		OKbutton = OKbutton.GetComponent<Button>();
		OKtext = OKtext.GetComponent<Text>();
	}

	public void closeImage()
	{
		Time.timeScale = 1.0f;
		tutorialImage.enabled = false;
		tutorialText.enabled = false;
		OKtext.enabled = false;
		OKbutton.enabled = false;
	}
}
