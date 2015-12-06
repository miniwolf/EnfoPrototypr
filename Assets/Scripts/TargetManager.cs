using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetManager : WaveManager
{
	public Text baseHealthText;
	private int maxTargetHitPoints;
	private GameObject[] enemiesToFind;
	private int enemyCount;
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
		enemiesToFind = GameObject.FindGameObjectsWithTag("Enemy");
		enemyCount = enemiesToFind.Length;

		// Winning condition
		if (waveCount == maxWaves && enemyCount == 0)
		{
			Application.LoadLevel("gameWon");
		}

		// Game Over condition
		if (targetHitPoints <= 0)
		{
			Application.LoadLevel("gameOver");
		}

		// Flashes red screen if base is hit
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
