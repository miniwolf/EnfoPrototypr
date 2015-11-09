using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionButtonScript : MonoBehaviour {

	private string description;

	void Start(){
		description = "Hola soy la descripcion de este item muchas gracias!";
	}

	public void setDescription(string desc){
		description = desc;
	}

	public void showDescription(){
		UIManager.showDescription(description);
	}

	public void stopShowDescription(){
		UIManager.stopShowDescription();
	}

}
