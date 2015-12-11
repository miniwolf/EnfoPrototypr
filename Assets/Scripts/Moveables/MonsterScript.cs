using UnityEngine;
using System.Collections;
using EnumExtension;

public class MonsterScript : Clickable {
	private NavigationComponent nav;
	private GameObject attackCircle;
	//private HealthComponent health;
	private NavMeshAgent agent;
	private Rigidbody rb;
	private CapsuleCollider capsuleCol;
	private float sinkSpeed;
	private bool rewardExp = true;


	private bool isRightClicked = false;

	public NavigationComponent Nav {
		set {
			nav = value;
		}
	}
	
	public HealthComponent Health {
		get
		{
			return health;
		}
	}

	// Use this for initialization
	void Awake() {
		sinkSpeed = 6f;
		agent = GetComponent<NavMeshAgent>();
		rb = GetComponent<Rigidbody>();
		capsuleCol = GetComponent<CapsuleCollider>();

		health = new HealthComponent();
		health.HealthBar = GetComponent<HealthBarScript>();
	}

	/*test*/
	private WarriorAnimation character;
	/*endoftest*/

	// Use this for initialization
	void Start() {
		/*test*/
		character = GameObject.Find("NinjaContainer").GetComponent<WarriorAnimation>();
		/*endOfTest*/
		attackCircle = gameObject.transform.FindChild("AttackCircle").gameObject;
		//health = new HealthComponent();
		//health.HealthBar = GetComponent<HealthBarScript>();
		experience = new ExperienceComponent();
		maxMana = 0;
		//health.MaxHealth = 15;
		currentMana = 0;
		//health.CurrentHealth = 15;
		characterName = "Orc";
		className = "Warrior";
		experience.CurrentLevel = 1;
		experience.MaxLevel = 1;
		experience.CurrentExp = 0;
		experience.MaxExp = 100;
		selectedCircle = gameObject.transform.FindChild("SelectedCircle").gameObject;
		picture = Resources.Load<Sprite>("Icons/orc");
	}

	void fadeCircle() {
		MeshRenderer mesh = attackCircle.GetComponent<MeshRenderer>();
		Color c = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, mesh.material.color.a - Time.deltaTime);
		mesh.material.color = c;
		if(mesh.material.color.a <= 0) {
			Color c2 = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 1);
			mesh.material.color = c2;
			attackCircle.SetActive(false);
			isRightClicked = false;
		}

	}

	void Update() {
		if (health.Health <= 0) {
			if (rewardExp) {
				character.Experience.addExp(101, character.Health, character.Attack);
				rewardExp = false;
			}
			SinkAndDestroy();
			return;
		}
		if(isRightClicked){
			fadeCircle();
		}
		nav.Update();
	}

	public void rightClicked() {
		if(isRightClicked == true){
			return;
		}
		attackCircle.SetActive(true);
		isRightClicked = true;
	}

	void OnTriggerEnter(Collider trigger) {
		nav.OnTriggerEnter(trigger);
	}

	void SinkAndDestroy() {
		agent.enabled = false;
		rb.isKinematic = true;
		capsuleCol.enabled = false;
		gameObject.transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
		Destroy(gameObject, 2f);
	}
}