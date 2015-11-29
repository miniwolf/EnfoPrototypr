using UnityEngine;
using System.Collections;

public class TargetManager : Clickable
{
	private bool gameOver = false;

	void Start()
	{
		targetHitPoint = 30;
	}

	// Update is called once per frame
	void Update ()
	{
		if(targetHitPoint <= 0)
		{
			gameOver = true;
		}
		if(gameOver && (Application.loadedLevel != 0 || Application.loadedLevel != 1))
		{
			Application.LoadLevel("gameOver");
		}
	}

	void OnTriggerEnter(Collider trigger)
	{
		if (trigger.gameObject.tag == "Enemy")
		{
			targetHitPoint -= 1;
			Destroy(trigger.gameObject);

			Debug.Log(targetHitPoint);
			Invoke("targetDamagedOSC", 0);
		}
	}


	void targetDamagedOSC()
	{
		float bubbles = 1.0f;
		OSCHandler.Instance.SendMessageToClient("SuperCollider", "/main/bubbles1", bubbles);
	}

}
