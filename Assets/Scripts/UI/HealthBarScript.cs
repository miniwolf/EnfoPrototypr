using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarScript {
	public Image healthBar; // This should link to the green bar
	public GameObject healthContainer;
	private Camera mainCamera;

	public HealthBarScript(Camera mainCamera, GameObject healthContainer, Image healthBar) {
		this.mainCamera = mainCamera;
		this.healthContainer = healthContainer;
		this.healthBar = healthBar;
	}

	public void SetHealth(float currentHealth, float maxHealth) {
		healthBar.fillAmount = currentHealth / maxHealth;
	}

	public void Update() { // Using the Billboard technique. Following the camera.
		healthContainer.transform.LookAt(healthContainer.transform.position + mainCamera.transform.rotation * Vector3.back,
					mainCamera.transform.rotation * Vector3.down);
	}
}
