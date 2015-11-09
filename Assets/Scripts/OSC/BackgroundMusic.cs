using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

    private float musicRepeatSecs = 180.0f;
    private float windRepeatSecs = 10.0f;


	// Use this for initialization
	void Start () {
        InvokeRepeating("musicOSC", 0, musicRepeatSecs);
        InvokeRepeating("windOSC", 0, windRepeatSecs);
    }

    // Update is called once per frame
    void Update () {

    }

    void musicOSC()
    {
        float music = 1.0f;
        OSCHandler.Instance.SendMessageToClient("SuperCollider", "/music", music);
    }

    void windOSC()
    {
        float wind = 1.0f;
        OSCHandler.Instance.SendMessageToClient("SuperCollider", "/wind", wind);
    }
}
