using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

	private float musicRepeatSecs = 180.0f;
	private float windRepeatSecs = 10.0f;


	// Use this for initialization
	void Awake () {
		Invoke("beatOSC", 0);
		InvokeRepeating("windOSC", 0, windRepeatSecs);
	}

	// Update is called once per frame
	void Update () {

	}

	void windOSC()
	{
		float wind = 1.0f;
		OSCHandler.Instance.SendMessageToClient("SuperCollider", "/main/wind1", wind);
	}

	void beatOSC()
	{
		float beat = 1.0f;
		OSCHandler.Instance.SendMessageToClient("SuperCollider", "/main/beat1", beat);
	}

}
