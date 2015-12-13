using UnityEngine;
using System.Collections;
using EnumExtension;

public class MonsterScript : Clickable {
	enum States {
		ATTACKING,
		IDLE,
		CHASING
	}

	private States state = States.IDLE;
	private GameObject attackCircle;
	private GameObject target;
	private Transform[] wayPoints;
	private NavMeshAgent agent;
	private bool rewardExp = true;
	private NavigationComponent navigationComponent;
	private AttackComponent attackComponent;
	private Animator animator;
	private WarriorAnimation player;
	private Transform playerTransform;
	private const int maxWaypoints = 6;
	private int currentWayPoint = 0;
	private bool isRightClicked = false;

	public Transform[] WayPoints {
		set {
			this.wayPoints = value;
		}
	}

	public GameObject Target {
		set {
			this.target = value;
		}
	}

	public AttackComponent Attack {
		get {
			return attackComponent;
		}
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
	public void Start() {
		Animator animator = GetComponent<Animator>();
		attackComponent = new AttackComponent(animator);
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		navigationComponent = new NavigationComponent(agent, animator);
		experienceComponent = new ExperienceComponent(healthComponent, attackComponent);
		maxMana = 0;
		currentMana = 0;
		characterName = "Orc";
		className = "Warrior";
		experienceComponent.CurrentLevel = 1;
		experienceComponent.MaxLevel = 1;
		experienceComponent.CurrentExp = 0;
		experienceComponent.MaxExp = 100;
		attackCircle = gameObject.transform.FindChild("AttackCircle").gameObject;
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

	public void Update() {
		if ( healthComponent.Health <= 0 ) {
			if ( rewardExp ) {
				player.Experience.addExp(101);
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
			navigationComponent.MoveTo(wayPoints[currentWayPoint].position);
		}
	}

	private void SinkAndDestroy() {
		float sinkSpeed = 6f;
		navigationComponent.Disable();
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<CapsuleCollider>().enabled = false;
		transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
		Destroy(gameObject, 2f);
	}
}