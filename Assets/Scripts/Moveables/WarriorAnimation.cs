using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace EnumExtension {
	public class State {
		public virtual void doMove(WarriorAnimation anim) {
			anim.move();
			anim.setCurrentState(new MOVE());
		}

		public virtual void attackMove(WarriorAnimation anim) {
			anim.move();
			anim.setCurrentState(new ATTACKMOVE());
		}

		public virtual void attack(WarriorAnimation anim, GameObject enemy) {
			anim.stop();
			anim.attack(enemy);
			anim.setCurrentState(new ATTACKING());
		}

		public virtual void chase(WarriorAnimation anim, GameObject enemy) {
			if ( enemy == null ) {
				enemy = anim.FindClosestEnemy();
			}
			anim.Enemy = enemy;
			anim.moveTo(enemy.transform.position);
			anim.setCurrentState(new CHASING());
		}
	}

	class ATTACKING : State { }
	class CHASING : State {
		public override void chase(WarriorAnimation anim, GameObject enemy) {
			if ( enemy != null ) {
				base.chase(anim, enemy);
			}
		}
		public override void attack(WarriorAnimation anim, GameObject enemy) {
			if ( anim.isInAttackRange(enemy) ) {
				base.attack(anim, enemy);
			}
		}
	}
	class ATTACKMOVE : State { }
	class IDLE : State { }
	class MOVE : State {
		public override void attack(WarriorAnimation anim, GameObject enemy) { }
		public override void chase(WarriorAnimation anim, GameObject enemy) {
			if ( enemy != null ) {
				base.chase(anim, enemy);
			}
		}
	}

	public enum Transitions {
		A,
		M1,
		AM0,
		NULL
	}

	public class WarriorAnimation : Clickable {
		public Animator animator;
		public GameObject healthBar;
		public Camera mainCamera;
		private NavMeshAgent agent;

		private State currentState = new IDLE();
		private Transitions transition = Transitions.NULL;
		private GameObject enemy;
		private AttackComponent attackComponent;
		private NavigationComponent navComponent;
		private List<GameObject> inAttackRange = new List<GameObject>();

		public ExperienceComponent Experience {
			get {
				return experienceComponent;
			}
		}

		public AttackComponent Attack {
			get {
				return attackComponent;
			}
		}

		public GameObject Enemy {
			get {
				return enemy;
			}
			set {
				enemy = value;
			}
		}

		void Awake() {
			agent = GetComponent<NavMeshAgent>();

			GameObject healthBarObject = (GameObject) Instantiate(healthBar, new Vector3(), new Quaternion());
			healthBarObject.transform.SetParent(transform);
			healthBarObject.transform.localPosition = new Vector3(0, 3.0f, 0);
			Image image = healthBarObject.GetComponentInChildren<Image>();
			healthComponent.HealthBar = new HealthBarScript(mainCamera, healthBarObject, image);
			attackComponent = new AttackComponent(animator);
			experienceComponent = new ExperienceComponent(healthComponent, attackComponent);
			navComponent = new NavigationComponent(agent, animator);

			healthComponent.MaxHealth = 100;
			healthComponent.CurrentHealth = 100;
			maxMana = 50;
			currentMana = 50;
			characterName = "Hattori";
			className = "Ninja";
			picture = Resources.Load<Sprite>("Icons/ninjaHead");
			buttons = null;
			selectedCircle = transform.FindChild("SelectedCircle").gameObject;
		}

		void ResetCharacter() {
			healthComponent.CurrentHealth = healthComponent.MaxHealth;
			transform.localPosition = new Vector3(5, 0, 100);
			moveTo(transform.position);
		}
		
		void Update() {
			if ( healthComponent.CurrentHealth <= 0 ) {
				ResetCharacter();
			}
			if ( isSelected() ) {
				switchTransition();
			}
			switchState();
			healthComponent.Update();
		}

		private void switchState() {
			if ( transition == Transitions.M1 ) {
				Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if ( Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Clickable")) &&
					hit.collider.tag == "Enemy") {
					GameObject tempEnemy = hit.collider.gameObject;
					tempEnemy.GetComponent<MonsterScript>().rightClicked();
					currentState.chase(this, tempEnemy);
				} else {
					currentState.doMove(this);
				}
			} else if ( transition == Transitions.AM0 ) {
				currentState.attackMove(this);
			} else if ( attackComponent.AttackRange ) {
				if ( !attackComponent.Attacking ) {
					currentState.attack(this, enemy);
				}
			} else if ( inRange(FindClosestEnemy(), navComponent.SeeRange) ) {
				currentState.chase(this, null);
			} else if ( checkType(typeof(ATTACKING)) ) {
				if ( enemy == null || enemy.GetComponent<MonsterScript>().Health.Health <= 0 ) {
					currentState = new IDLE();
				}
			}
			if ( checkType(typeof(CHASING)) ) {
				navComponent.MoveTo(enemy.transform.position);
			}
			if ( checkType(typeof(ATTACKMOVE)) || checkType(typeof(MOVE)) ) {
				if ( navComponent.ReachedDestination() ) {
					navComponent.Stop();
					currentState = new IDLE();
				}
			}
		}

		private bool checkType(System.Type type) {
			return currentState.GetType().Equals(type);
		}

		private void switchTransition() {
			switch (transition) {
			case Transitions.A:
				if ( Input.GetMouseButtonDown(0) ) {
					transition = Transitions.AM0;
				} else if ( Input.GetKeyDown("a") ) {
					transition = Transitions.NULL;
				}
				break;
			case Transitions.AM0:
			case Transitions.M1:
				transition = Transitions.NULL;
				break;
			case Transitions.NULL:
				if ( Input.GetKeyDown("a") ) {
					transition = Transitions.A;
				} else if ( Input.GetMouseButtonDown(1) ) {
					transition = Transitions.M1;
				}
				break;
			}
		}

		// TODO: Look at notes for better solution
		public GameObject FindClosestEnemy() {
			// Find all game objects with tag Enemy
			GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");

			// Iterate through them and find the closest one
			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
			GameObject closest = null;
			foreach (GameObject go in gos) {
				if ( go.GetComponent<MonsterScript>().Health.CurrentHealth <= 0 ) {
					continue;
				}
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if ( curDistance < distance ) { 
					closest = go;
					distance = curDistance; 
				}
			}
			return closest;
		}

		public bool isInAttackRange(GameObject target) {
			return inAttackRange.Contains(target);
		}

		private bool inRange(GameObject target, float range) {
			return target == null ? false : 
				Vector3.Distance(transform.position, target.transform.position) <= range;
		}

		public void setCurrentState (State state) {
			this.currentState = state;
		}

		public void move() {
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if ( Physics.Raycast(ray, out hit) ) {
				animator.SetBool("Running", true);
				agent.SetDestination(hit.point);
			}
		}

		public void attack(GameObject enemy) {
			Debug.Log("Try getting swarmed");
			if(enemy == null) {
				return;
			}
			attackComponent.attack(enemy.GetComponent<MonsterScript>().Health);
			StartCoroutine(startAttackTimer());
		}
		
		public void stop() {
			navComponent.MoveTo(transform.position);
			animator.SetBool("Running", false);
		}

		public void moveTo(Vector3 position) {
			navComponent.MoveTo(position);
		}

		private IEnumerator startAttackTimer() {
			yield return new WaitForSeconds(attackComponent.StunTime);
			attackComponent.Attacking = false;
		}

		void FixedUpdate() {
			attackComponent.AttackRange = false;
			inAttackRange.Clear();
		}

		void OnTriggerEnter(Collider other) {
			if ( other.tag == "Enemy" && other.gameObject.GetComponent<MonsterScript>().Health.CurrentHealth > 0 ) {
				attackComponent.AttackRange = true;
				inAttackRange.Add(other.gameObject);
			}
		}

		void OnTriggerStay(Collider other) {
			if ( other.tag == "Enemy" && other.gameObject.GetComponent<MonsterScript>().Health.CurrentHealth > 0 ) {
				attackComponent.AttackRange = true;
				inAttackRange.Add(other.gameObject);
			}
		}
	}
}
