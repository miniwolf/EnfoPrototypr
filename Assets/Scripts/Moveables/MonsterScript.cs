using UnityEngine;
using System.Collections;
using EnumExtension;

public class MonsterScript : Clickable {
	enum States {
		ATTACKING,
		IDLE,
		CHASING
	}
	
	private GameObject attackCircle;
	private NavMeshAgent agent;
	private Rigidbody rb;
	private CapsuleCollider capsuleCol;
	private float sinkSpeed;
	private bool rewardExp = true;
	private States state = States.IDLE;
	private NavigationComponent navigationComponent;
	private AttackComponent attackComponent;
	private Animator animator;
	private WarriorAnimation player;
	private Transform playerTransform;
	private GameObject target;
	private Transform[] wayPoints;
	private const int maxWaypoints = 6;
	private int currentWayPoint = 0;

<<<<<<< HEAD
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
=======
	public Transform[] WayPoints {
		set {
			this.wayPoints = value;
		}
	}

	public GameObject Target {
		set {
			this.target = value;
		}
>>>>>>> Modify characters to be self sustaining whilst not selected.
	}

	public WarriorAnimation Player {
		set {
			this.player = value;
			this.playerTransform = player.transform;
		}
	}

	public NavigationComponent NavComponent {
		set {
			this.navigationComponent = value;
		}
	}

	// Use this for initialization
	void Start() {
<<<<<<< HEAD
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
		
=======
		animator = GetComponent<Animator>();
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		attackComponent = new AttackComponent(animator);
		navigationComponent = new NavigationComponent(agent, animator);
		navigationComponent.SeeRange = 7;
		currentMana = 0;
		maxMana = 0;
		healthComponent.CurrentHealth = 20;
		healthComponent.MaxHealth = 20;
		characterName = "Arthas";
		className = "Dark Knight";
		experienceComponent.CurrentLevel = 1;
		experienceComponent.MaxLevel = 1;
		experienceComponent.CurrentExp = 0;
		experienceComponent.MaxExp = 100;
		selectedCircle = this.gameObject.transform.FindChild("SelectedCircle").gameObject;
		picture = Resources.Load<Sprite>("Icons/arthas");
	}

	public void Update() {
		if ( health.Health <= 0 ) {
			if ( rewardExp ) {
				player.Experience.addExp(101, player.Health, player.Attack);
				rewardExp = false;
			}
			SinkAndDestroy();
			return;
		}
		if ( isRightClicked ) {
			fadeCircle();
		}
		if ( animator && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") ) {
			return;
		}
		state = inRange(); // Updating state
		switch (state) {
		case States.ATTACKING:
			if ( !attackComponent.Attacking ) {
				navigationComponent.MoveTo(transform.position);
				navigationComponent.Stop();
				attackComponent.attack(player.Health);
				StartCoroutine(startAttackTimer());
			}
			break;
		case States.CHASING:
			navigationComponent.MoveTo(playerTransform.position);
			break;
		case States.IDLE:
			idle();
			break;
		}
		healthComponent.Update();
	}

	void FixedUpdate() {
		attackComponent.AttackRange = false;
	}

	public void rightClicked() {
		if(isRightClicked == true){
			return;
		}
		attackCircle.SetActive(true);
		isRightClicked = true;
	}

	void OnTriggerEnter(Collider trigger) {
		if ( trigger.tag == "WayPoint" ) {
			currentWayPoint++;
		} else if ( trigger.tag == "Player" ) {
			attackComponent.AttackRange = true;
		}
	}

	void OnTriggerStay(Collider other) {
		if ( other.tag == "Player" ) {
			attackComponent.AttackRange = true;
		}
	}

	private IEnumerator startAttackTimer() {
		yield return new WaitForSeconds(attackComponent.StunTime);
		attackComponent.Attacking = false;
	}

	private States inRange() {
		if ( attackComponent.AttackRange ) {
			return States.ATTACKING;
		}

		float distance = Vector3.Distance(transform.position, playerTransform.position);
		if (distance <= navigationComponent.SeeRange) {
			return States.CHASING;
		} else {
			return States.IDLE;
		}
	}
	
	// Update is called once per frame
	private void idle() {
		if ( currentWayPoint >= maxWaypoints ) {
			navigationComponent.MoveTo(target.transform.position);
		} else {
			navigationComponent.MoveTo(wayPoints[currentWayPoint].transform.position);
		}
	}

	void SinkAndDestroy() {
		//agent.enabled = false;
		rb.isKinematic = true;
		capsuleCol.enabled = false;
		gameObject.transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
		Destroy(gameObject, 2f);
	}
}