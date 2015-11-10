using UnityEngine;
using System.Collections;

public class MainCameraMovementScript : MonoBehaviour {

	private float speed = 100.0f;
	private float speed2 = 85.0f;
	void Update()
	{
		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.position += new Vector3(speed * Time.deltaTime,0,0);
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.position -= new Vector3(speed * Time.deltaTime,0,0);
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position += new Vector3(0,0,speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.position -= new Vector3(0,0,speed * Time.deltaTime);
		}


		if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // forward
		{
			if(transform.position.y < 75){
				transform.position += new Vector3(0,speed2 * Time.deltaTime,0);
			}
		}

		else if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // backwards
		{
			if(transform.position.y > 25){
				transform.position -= new Vector3(0,speed2 * Time.deltaTime,0);
			}

		}
	}
}
