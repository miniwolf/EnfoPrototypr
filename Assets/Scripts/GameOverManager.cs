using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    private bool gameOver = false;
    public static int targetLife = 20;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(targetLife <= 0)
        {
            gameOver = true;
        }
        if(gameOver && (Application.loadedLevel != 0 || Application.loadedLevel != 1))
        {
            Application.LoadLevel("gameOver");
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.gameObject.tag == "EnemyLeft" || col.collider.gameObject.tag == "EnemyRight")
        {
            targetLife -= 1;
            Destroy(col.collider.gameObject);
            Debug.Log(targetLife);
        }
    }
}
