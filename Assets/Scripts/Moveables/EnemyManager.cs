using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public float waveTime = 90f;
    private bool callStart = false;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Update()
    {
        switch(callStart)
        {
            case true:
                if(!countDownTimer.isSpawning)
                {
                    callStart = false;
                    Start();
                }
                break;
            case false:
                if(countDownTimer.isSpawning)
                {
                    callStart = true;
                    CancelInvoke();
                }
                break;
        }
    }

    void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
