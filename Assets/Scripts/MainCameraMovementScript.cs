using UnityEngine;
using System.Collections;

public class MainCameraMovementScript : MonoBehaviour {
	
	private float speed = 100.0f;
	private float speed2 = 85.0f;
	private Vector3 mousePosition;
	
	private int upLimit = 155;
	private int downLimit = -20;
	private int rightLimit = 30;
	private int leftLimit = 175;
	
	void Update() {
		
		/*Camera movement with middle mouse button*/
		if (Input.GetMouseButtonDown(2)){
			mousePosition = Input.mousePosition;
		}
		if(Input.GetMouseButton(2)){
			if(Input.mousePosition.y - mousePosition.y > 0 && (transform.position.x < upLimit)){//going up
				transform.position += new Vector3((Input.mousePosition.y - mousePosition.y)*Time.deltaTime,0,0);
			}else if(Input.mousePosition.y - mousePosition.y < 0 && transform.position.x > downLimit){//going down
				transform.position += new Vector3((Input.mousePosition.y - mousePosition.y)*Time.deltaTime,0,0);
			}
			if(Input.mousePosition.x - mousePosition.x > 0 && transform.position.z > rightLimit){ //going right
				transform.position += new Vector3(0,0,(mousePosition.x-Input.mousePosition.x)*Time.deltaTime);
			}else if(Input.mousePosition.x - mousePosition.x < 0 && transform.position.z < leftLimit){ //going left
				transform.position += new Vector3(0,0,(mousePosition.x-Input.mousePosition.x)*Time.deltaTime);
			}
			//transform.position += new Vector3((Input.mousePosition.y - mousePosition.y)*Time.deltaTime,0,(mousePosition.x-Input.mousePosition.x)*Time.deltaTime );
		}
		/*Camera movement with mouse movement*/
		if(Input.mousePosition.x < 30 && transform.position.z < leftLimit){ //left
			transform.position += new Vector3(0,0,speed * Time.deltaTime);
		}
		if(Input.mousePosition.x > Screen.width -30 && transform.position.z > rightLimit){ //right
			transform.position -= new Vector3(0,0,speed * Time.deltaTime);
		}
		if(Input.mousePosition.y < 10 && (transform.position.x > downLimit)){ //down
			transform.position -= new Vector3(speed * Time.deltaTime,0,0);
		}
		if(Input.mousePosition.y > Screen.height -30  && (transform.position.x < upLimit)){//up
			transform.position += new Vector3(speed * Time.deltaTime,0,0);
		}
		
		/*Camera movement with direction keys*/
		if(Input.GetKey(KeyCode.UpArrow) && (transform.position.x < upLimit))
		{
			transform.position += new Vector3(speed * Time.deltaTime,0,0);
		}
		if(Input.GetKey(KeyCode.DownArrow) && (transform.position.x > downLimit))
		{
			transform.position -= new Vector3(speed * Time.deltaTime,0,0);
		}
		if(Input.GetKey(KeyCode.LeftArrow) && transform.position.z < leftLimit)
		{
			transform.position += new Vector3(0,0,speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.RightArrow) && transform.position.z > rightLimit)
		{
			transform.position -= new Vector3(0,0,speed * Time.deltaTime);
		}
		
		
		/*Camera zoom with mouse wheel*/
		if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // forward
		{
			if(transform.position.y < 40){
				transform.position += new Vector3(0,speed2 * Time.deltaTime,0);
			}
		}
		
		if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // backwards
		{
			if(transform.position.y > 25){
				transform.position -= new Vector3(0,speed2 * Time.deltaTime,0);
			}
			
		}
	}
}
