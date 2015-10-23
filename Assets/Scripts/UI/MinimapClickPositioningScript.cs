using UnityEngine;
using System.Collections;

public class MinimapClickPositioningScript : MonoBehaviour {

	private RaycastHit hit;
	private Ray ray;
	private Camera minimapCamera;
	private Camera mCamera;
	public GameObject mainCamera;

	void Awake(){
		minimapCamera = gameObject.GetComponent<Camera>();
		mCamera = mainCamera.GetComponent<Camera>();
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			this.ray = minimapCamera.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out this.hit, Mathf.Infinity, LayerMask.GetMask("Ground"))){
				mainCamera.transform.position = new Vector3(hit.point.x - mCamera.orthographicSize*5, mainCamera.transform.position.y, this.hit.point.z);
			}
		}
	}
}
