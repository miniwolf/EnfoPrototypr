using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OldActionInspectorScript : MonoBehaviour {
	
	/*List to access the buttons after adding them*/
	private List<GameObject> buttons;

	public void Awake(){
		buttons = new List<GameObject> ();
		
		//test
		Sprite s;
		s = Resources.Load<Sprite> ("Icons/Weapons_Sword");
		addButton (s);
		s= Resources.Load<Sprite> ("Icons/Weapons_Hammer");
		addButton (s);
		addButton (s);
		addButton (s);
		addButton (s);
		addButton (s);
		addButton (s);
		addButton (s);
		GameObject go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		addButton (go);
		
	}
	/*In case we instantiate the button*/
	void addButton(GameObject go){
		go.transform.SetParent (gameObject.transform,true);
		go.transform.localScale += new Vector3(1,1,1);
		buttons.Add (go);
		
	}
	
	/*In case we only have the sprite*/
	void addButton(Sprite sprite){
		GameObject go;
		Button b;
		//Instantiates the button
		go=(GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		//Sets the button as child of the inspector
		go.transform.SetParent(transform,true);
		//Ehen instantiated, its scale is 0,0,0 default, we correct it
		go.transform.localScale += new Vector3(1,1,1);
		b = go.GetComponent<Button> ();
		//changes image of the button
		b.image.sprite = sprite;
		
		buttons.Add (go);
		
	}
	
}