using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

    private float seconds = 180.0f;

	// Use this for initialization
	void Start () {
        InvokeRepeating("musicOSC", 0, seconds);
    }

    // Update is called once per frame
    void Update () {

    }

    void musicOSC()
    {
        float music = 1.0f;
        OSCHandler.Instance.SendMessageToClient("SuperCollider", "/music", music);
    }
}
