using UnityEngine;
using System.Collections;

public class NavigationFactory : MonoBehaviour {
	public static void CreateLeftEnemy(MonsterScript monster, Transform player) {
		Transform[] wayPoints = new Transform[6];
		wayPoints[0] = GameObject.Find("WayPoint1L").transform;
		wayPoints[1] = GameObject.Find("WayPoint2L").transform;
		wayPoints[2] = GameObject.Find("WayPoint3L").transform;
		wayPoints[3] = GameObject.Find("WayPoint4L").transform;
		wayPoints[4] = GameObject.Find("WayPoint5L").transform;
		wayPoints[5] = GameObject.Find("WayPoint6L").transform;
		
		GameObject target = GameObject.Find("Target");
		NavigationComponent component = new NavigationComponent(monster.GetComponent<NavMeshAgent>(), monster, wayPoints, target, player);
		monster.Nav = component;
	}

	public static void CreateRightEnemy(MonsterScript monster, Transform player) {
		Transform[] wayPoints = new Transform[6];
		wayPoints[0] = GameObject.Find("WayPoint1R").transform;
		wayPoints[1] = GameObject.Find("WayPoint2R").transform;
		wayPoints[2] = GameObject.Find("WayPoint3R").transform;
		wayPoints[3] = GameObject.Find("WayPoint4R").transform;
		wayPoints[4] = GameObject.Find("WayPoint5R").transform;
		wayPoints[5] = GameObject.Find("WayPoint6R").transform;
		
		GameObject target = GameObject.Find("Target");
		NavigationComponent component = new NavigationComponent(monster.GetComponent<NavMeshAgent>(), monster, wayPoints, target, player);
		monster.Nav = component;
	}
}
