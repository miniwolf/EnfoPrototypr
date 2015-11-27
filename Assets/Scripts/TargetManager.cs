using UnityEngine;
using System.Collections;

public class TargetManager : MonoBehaviour
{
	public static int targetLife = 30;
	private bool gameOver = false;
	public static bool targetHit = false;

	// Update is called once per frame
	void Update ()
	{
		if(targetLife <= 0)
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
		if (trigger.gameObject.tag == "EnemyLeft" || trigger.gameObject.tag == "EnemyRight")
		{
			targetLife -= 1;
			Invoke("targetDamagedOSC", 0);
			Debug.Log(targetLife);
		}
	}

	void targetDamagedOSC()
	{
		float bubbles = 1.0f;
		OSCHandler.Instance.SendMessageToClient("SuperCollider", "/main/bubbles1", bubbles);
	}

}
