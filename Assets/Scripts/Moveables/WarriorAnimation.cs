using UnityEngine;
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
		NULL
	}

	public class WarriorAnimation : Clickable {
		public Animator animator;
		public float seeRange = 7;
		public float attackRange = 3f;
		public float damage = 10;
		public float stunTime = 1.6f;

		private State currentState = new IDLE();
		private Transitions transition = Transitions.NULL;
		private NavMeshAgent agent;
		private GameObject enemy;
		private bool attacking = false;

		//Warrior types
		public enum Warrior{Karate, Ninja, Brute, Sorceress};

		public Warrior warrior;

		void Start() {
			agent = gameObject.GetComponent<NavMeshAgent>();
			maxHealth = 100;
			currentHealth = 100;
			maxMana = 50;
			currentMana = 50;
			maxExp = 100;
			currentExp = 0;
			maxLevel = 20;
			currentLevel = 1;
			
			characterName = "Hattori";
			className = "Ninja";
			picture = Resources.Load<Sprite>("Icons/dragon");
			buttons = null;
			selectedCircle = this.gameObject.transform.FindChild("SelectedCircle").gameObject;
		}
		
		void Update() {
			// TODO: Check first if selected
			if(!selectedCircle.activeSelf){
				return;
			}
			switchState();
			if ( Input.anyKeyDown ) {
				switchTransition();
			}
		}

		private void switchState() {
			if ( Input.GetMouseButton(1) ) {
				currentState.doMove(this);
			} else if ( Input.GetMouseButtonDown(0) && transition == Transitions.A ) {
				currentState.attackMove(this);
			} else if ( inRange(enemy, attackRange) ) {
				if ( !attacking ) {
					currentState.attack(this);
				}
			} else if ( inRange(enemy, seeRange) ) {
				currentState.chase(this, enemy.transform);
			} else if ( checkType(typeof(ATTACKING)) ) {
				currentState = new IDLE();
				if ( enemy == null || enemy.GetComponent<MonsterScript>().Health.Health <= 0 ) {
					currentState = new IDLE();
				}
			}
			if ( checkType(typeof(ATTACKMOVE)) || checkType(typeof(MOVE)) ) {
				if ( V2Equal() ) {
					stop();
					currentState = new IDLE();
				}
			}
		}

		private bool checkType (System.Type type) {
			return currentState.GetType().Equals(type);
		}

		private void switchTransition() {
			switch (transition) {
			case Transitions.A:
				if ( !Input.GetKeyDown("a") ) {
					transition = Transitions.NULL;
				}
				break;
			case Transitions.NULL:
				if ( Input.GetKeyDown("a") ) {
					transition = Transitions.A;
				}
				break;
			}
		}

		// TODO: Look at notes for better solution
		private GameObject FindClosestEnemy() {
			// Find all game objects with tag Enemy
			GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy"); 

			float distance = Mathf.Infinity; 
			Vector3 position = transform.position; 
			// Iterate through them and find the closest one
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

		private bool V3Equal() {
			if (!agent.pathPending && 
			    agent.remainingDistance <= agent.stoppingDistance && 
			    (agent.velocity.sqrMagnitude == 0f)) {

				return true;
			}
			Debug.Log(agent.velocity.sqrMagnitude);
			return false;
		}

		private bool V2Equal() {
			if ( !agent.pathPending ) {
				return agent.pathStatus == NavMeshPathStatus.PathComplete && !agent.hasPath;
			}
			return false;
		}

		private IEnumerator startAttackTimer() {
			yield return new WaitForSeconds(stunTime);
			attacking = false;
		}

		public void setCurrentState (State state) {
			this.currentState = state;
		}

		public void move() {
			// If the click was on an object then set the agent's
			// destination to the point where the click occurred.
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if ( Physics.Raycast(ray, out hit) ) {
				agent.SetDestination(hit.point);
				animator.SetBool("Running", true);
			}
		}

		public void attack() {
			animator.SetTrigger("Attack1Trigger");
			enemy.GetComponent<MonsterScript>().Health.GetHit(damage);
			attacking = true;
			StartCoroutine(startAttackTimer());
		}
		
		public void stop() {
			animator.SetBool("Running", false);
		}

		public void moveTo(Vector3 position) {
			agent.SetDestination(position);
		}
	}
}
