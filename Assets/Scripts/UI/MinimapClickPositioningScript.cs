using UnityEngine;
using System.Collections;

public class MinimapClickPositioningScript : MonoBehaviour {

	private RaycastHit hit;
	private Ray ray;
	private Camera minimapCamera;

	void Awake(){
		minimapCamera = gameObject.GetComponent<Camera>();
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			if(Physics.Raycast(ray, out this.hit, Mathf.Infinity, LayerMask.GetMask("Ground"))){

			}
		}
	}
}
