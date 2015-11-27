using UnityEngine;
using System.Collections;

public class StartMenuMusic : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		Invoke("startMenuMusicOSC", 0);
	}


	void startMenuMusicOSC()
	{
		float startMenuMusic = 1.0f;
		OSCHandler.Instance.SendMessageToClient("SuperCollider", "/main/music2", startMenuMusic);
	}
}