using UnityEngine;
using System.Collections;
using EnumExtension;

public class NavigationFactory : MonoBehaviour {
	public static void CreateLeftEnemy(MonsterScript monster, WarriorAnimation player) {
		Transform[] wayPoints = new Transform[6];
		wayPoints[0] = GameObject.Find("WayPoint1L").transform;
		wayPoints[1] = GameObject.Find("WayPoint2L").transform;
		wayPoints[2] = GameObject.Find("WayPoint3L").transform;
		wayPoints[3] = GameObject.Find("WayPoint4L").transform;
		wayPoints[4] = GameObject.Find("WayPoint5L").transform;
		wayPoints[5] = GameObject.Find("WayPoint6L").transform;
		monster.WayPoints = wayPoints;
	}

	public static void CreateRightEnemy(MonsterScript monster, WarriorAnimation player) {
		Transform[] wayPoints = new Transform[6];
		wayPoints[0] = GameObject.Find("WayPoint1R").transform;
		wayPoints[1] = GameObject.Find("WayPoint2R").transform;
		wayPoints[2] = GameObject.Find("WayPoint3R").transform;
		wayPoints[3] = GameObject.Find("WayPoint4R").transform;
		wayPoints[4] = GameObject.Find("WayPoint5R").transform;
		wayPoints[5] = GameObject.Find("WayPoint6R").transform;
		monster.WayPoints = wayPoints;
	}
}
