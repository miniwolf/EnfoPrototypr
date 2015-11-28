using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarScript : MonoBehaviour {
	public Image healthBar; // This should link to the green bar
	public GameObject healthContainer;
	private Camera mainCamera;

	public Camera MainCamera {
		set {
			mainCamera = value;
		}
	}

	public void SetHealth(float currentHealth, float maxHealth) {
		SetHealthBar(currentHealth / maxHealth);
	}

	void Update() { // Using the Billboard technique. Following the camera.
		healthContainer.transform.LookAt(healthContainer.transform.position + mainCamera.transform.rotation * Vector3.back,
		                 mainCamera.transform.rotation * Vector3.down);
	}

	private void SetHealthBar(float myHealth){
		healthBar.fillAmount = myHealth;
	}
}
