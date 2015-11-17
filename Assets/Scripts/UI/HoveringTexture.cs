using UnityEngine;
using System.Collections;

public class HoveringTexture : MonoBehaviour {
	private Camera cam = Camera.main;
	private float clampBorderSize = 0.05f;
	private bool clampToScreen = false;
	private Vector3 offset = Vector3.up; // Short hand for Vector3(0,1,0). Getting the up world vector.
	
	private Transform target;
	private Transform transform;
	private Transform camTransform = Camera.main.transform;

	// Use this for initialization
	void Start() {
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update() {
		if ( clampToScreen ) {
			Vector3 relativePosition = camTransform.InverseTransformPoint(target.position);
			relativePosition.z = Mathf.Max(relativePosition.z, 1.0f);
			transform.position = cam.WorldToViewportPoint(camTransform.TransformPoint(relativePosition + offset));
			transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampBorderSize, 1.0f - clampBorderSize),
			                             Mathf.Clamp(transform.position.y, clampBorderSize, 1.0f - clampBorderSize),
			                             transform.position.z);
		} else {
			transform.position = cam.WorldToViewportPoint(target.position + offset);
		}
	}
}
