using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetManager : LevelManager
{
	public Text baseHealthText;
	private bool gameOver = false;
	private int maxTargetHitPoints;
	bool damaged;
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.9f);


	void Start()
	{
		targetHitPoints = 10;
		maxTargetHitPoints = targetHitPoints;
		baseHealthText.text = "Base " + maxTargetHitPoints.ToString() + "/" + maxTargetHitPoints.ToString();
	}

	// Update is called once per frame
	void Update ()
	{
		if(targetHitPoints <= 0)
		{
			gameOver = true;
		}
		if(gameOver && (Application.loadedLevel != 0 || Application.loadedLevel != 1))
		{
			Application.LoadLevel("gameOver");
		}

		if (damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	void OnTriggerEnter(Collider trigger)
	{
		if (trigger.gameObject.tag == "Enemy")
		{
			damaged = true;
			targetHitPoints -= 1;
			Destroy(trigger.gameObject);
			baseHealthText.text = "Base " + targetHitPoints.ToString() + "/" + maxTargetHitPoints.ToString();
			Invoke("targetDamagedOSC", 0);
		}
	}


	void targetDamagedOSC()
	{
		float targetHit = 1.0f;
		OSCHandler.Instance.SendMessageToClient("SuperCollider", "/main/targetHit1", targetHit);
	}

}
