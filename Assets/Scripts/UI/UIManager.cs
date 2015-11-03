using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private RaycastHit hit;
	private Ray ray;
	private Camera mCamera;
	private Clickable click;

	public Text healthText;
	public Text manaText;
	public Text name;
	public Image picture;

	public GameObject mainCamera;
	public ActionInspectorScript actionInspector;

	// Use this for initialization
	void Start () {
		mCamera = mainCamera.GetComponent<Camera>();
		DeactivateInfo();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			this.ray = mCamera.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Clickable"))){
				DeactivateInfo();
				actionInspector.ResetButtons();
				click = hit.transform.gameObject.GetComponent<Clickable>();
				healthText.text = click.getCurrentHealth() + "/" + click.getMaxHealth();
				manaText.text = click.getCurrentMana() + "/" + click.getMaxMana();
				name.text = click.getName();
				picture.sprite = click.getPicure();
				actionInspector.AddAllButtons(click.getButtons());
			}
		}
	}


	void DeactivateInfo(){

		picture.sprite = Resources.Load<Sprite>("Icons/slot");
		healthText.text = "0/0";
		manaText.text = "0/0";
		name.text = "";
	}

}
