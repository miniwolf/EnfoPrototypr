using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace EnumExtension {
	public class State {
		public virtual void doMove(WarriorAnimation anim) {
			//if (hit a creature) {
			// state = States.ATTACKING;
			//} else {
			anim.move();
			anim.setCurrentState(new MOVE());
			//}
		}

		public virtual void attackMove(WarriorAnimation anim) {
			anim.move();
			anim.setCurrentState(new ATTACKMOVE());
		}

		public virtual void attack(WarriorAnimation anim) {
			anim.stop();
			anim.attack();
			anim.setCurrentState(new ATTACKING());
		}

		public virtual void chase(WarriorAnimation anim, Transform target) {
			anim.moveTo(target.position);
			anim.setCurrentState(new ATTACKING());
		}
	}

	class ATTACKING : State { }
	class ATTACKMOVE : State { }
	class IDLE : State { }
	class MOVE : State {
		public override void attack(WarriorAnimation anim) { }
		public override void chase(WarriorAnimation anim, Transform target) { }
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

		public AttackComponent Attack {
			get {
				return attackComponent;
			}
		}

		void Start() {
			agent = GetComponent<NavMeshAgent>();

			GameObject healthBarObject = (GameObject) Instantiate(healthBar, new Vector3(), new Quaternion());
			healthBarObject.transform.SetParent(transform);
			healthBarObject.transform.localPosition = new Vector3(0, 3.0f, 0);
			Image image = healthBarObject.GetComponentInChildren<Image>();
			healthComponent.HealthBar = new HealthBarScript(mainCamera, healthBarObject, image);
			experienceComponent = new ExperienceComponent();
			attackComponent = new AttackComponent(animator);
			navComponent = new NavigationComponent(agent, animator);

			healthComponent.MaxHealth = 100;
			healthComponent.CurrentHealth = 100;
			maxMana = 50;
			currentMana = 50;
			characterName = "Hattori";
			className = "Ninja";
			picture = Resources.Load<Sprite>("Icons/ninjaHead");
			buttons = null;
			selectedCircle = this.gameObject.transform.FindChild("SelectedCircle").gameObject;
		}
		
		void Update() {
			if ( selectedCircle.activeSelf ) {
				switchTransition();
			}
			switchState();
			Health.Update();
		}

		private void switchState() {
			if ( transition == Transitions.M1 ) {
				currentState.doMove(this);
			} else if ( transition == Transitions.AM0 ) {
				currentState.attackMove(this);
			} else if ( attackComponent.AttackRange ) {
				if ( !attackComponent.Attacking ) {
					currentState.attack(this);
				}
			} else if ( inRange(enemy, navComponent.SeeRange) ) {
				currentState.chase(this, enemy.transform);
			} else if ( checkType(typeof(ATTACKING)) ) {
				currentState = new IDLE();
				if ( enemy == null || enemy.GetComponent<MonsterScript>().Health.Health <= 0 ) {
					currentState = new IDLE();
				}
			}
			if ( checkType(typeof(ATTACKMOVE)) || checkType(typeof(MOVE)) ) {
				if ( navComponent.ReachedDestination() ) {
					stop();
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
		private GameObject FindClosestEnemy() {
			// Find all game objects with tag Enemy
			GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");

			// Iterate through them and find the closest one
			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
			GameObject closest = null;
			foreach (GameObject go in gos) {
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if ( curDistance < distance ) { 
					closest = go;
					distance = curDistance; 
				}
			}
			return closest;
		}

		private bool inRange(GameObject target, float range) {
			if ( target == null || !rangeCheck(target.transform, range) ) {
				enemy = FindClosestEnemy();
				return false;//rangeCheck(enemy.transform, range);
			}
			return true;
		}

		private bool rangeCheck(Transform enemyTransform, float range) {
			return Vector3.Distance(transform.position, enemyTransform.position) <= range;
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

		public void attack() {
			animator.SetTrigger("Attack1Trigger");
			StartCoroutine(startAttackTimer());
			enemy = FindClosestEnemy();
			attackComponent.attack(enemy.GetComponent<MonsterScript>().Health);
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
		}

		void OnTriggerEnter(Collider other) {
			if ( other.tag == "Enemy" ) {
				attackComponent.AttackRange = true;
			}
		}

		void OnTriggerStay(Collider other) {
			if ( other.tag == "Enemy" ) {
				attackComponent.AttackRange = true;
			}
		}
	}
}
