using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {
	public GameObject healthBar; // This should link to the green bar

	public void SetHealth(float currentHealth, float maxHealth) {
		SetHealthBar(currentHealth / maxHealth);
	}

	private void SetHealthBar(float myHealth){
		healthBar.transform.localScale = new Vector3(Mathf.Clamp(2.0f * myHealth,0f ,2f), 
		                                             healthBar.transform.localScale.y, 
		                                             healthBar.transform.localScale.z);
	}
}
