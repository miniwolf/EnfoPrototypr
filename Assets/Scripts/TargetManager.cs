using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TargetManager : MonoBehaviour {
	public Text baseHealthText;
	public Image damageImage;
	private int maxTargetHitPoints;
	private int targetHitPoints;
	private float flashSpeed = 5f;
	private Color flashColour = new Color(1f, 0f, 0f, 0.9f);

	void Start() {
		targetHitPoints = 10;
		maxTargetHitPoints = targetHitPoints;
		baseHealthText.text = "Base " + maxTargetHitPoints.ToString() + "/" + maxTargetHitPoints.ToString();
	}

	// Update is called once per frame
	void Update () {
		// Winning condition
		int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
		if ( WaveManager.WaveCount == WaveManager.MaxWaves && enemyCount == 0 ) {
			SceneManager.LoadScene("gameWon");
		}

		// Fades red screen if base is hit
		damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider trigger) {
		if ( trigger.gameObject.tag == "Enemy" ) {
			damageImage.color = flashColour;

			// Game Over condition
			targetHitPoints--;
			if ( targetHitPoints <= 0 ) {
				SceneManager.LoadScene("gameOver");
			}

			Destroy(trigger.gameObject);
			baseHealthText.text = "Base " + targetHitPoints.ToString() + "/" + maxTargetHitPoints.ToString();
			Invoke("targetDamagedOSC", 0);
		}
	}

	void targetDamagedOSC() {
		float targetHit = 1.0f;
		OSCHandler.Instance.SendMessageToClient("SuperCollider", "/main/targetHit1", targetHit);
	}
}
