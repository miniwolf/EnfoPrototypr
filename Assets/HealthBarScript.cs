using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {
	private float maxHealth = 100f;
	private float currentHealth = 100f;
	public GameObject healthBar; // This should link to the green bar


	// Use this for initialization
	void Start() {
		InvokeRepeating("DecreaseHealth", 1f, 1f);// TODO: This is just a debug
	}

	void DecreaseHealth() {
		currentHealth -= 2f;
		float computedHealth = currentHealth / maxHealth;
		SetHealthBar(computedHealth);
	}

	public void SetHealthBar(float myHealth){
		healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth,0f ,1f), 
		                                             healthBar.transform.localScale.y, 
		                                             healthBar.transform.localScale.z);
	}
}
