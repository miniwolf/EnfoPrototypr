using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public float waveTime = 90f;

    public int counter = 0;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
