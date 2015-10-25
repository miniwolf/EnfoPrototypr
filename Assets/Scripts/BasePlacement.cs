using UnityEngine;
using System.Collections;

public class BasePlacement : MonoBehaviour {

    public GameObject homebase;

    // Default base position and rotation
    public bool setDefault = true;

    // Position and rotation parameters
    public float xCoord, yCoord, zCoord;
    public float xRot, yRot, zRot;

    void Start () {
        Invoke("SpawnBase", 0);
    }

    void SpawnBase()
    {
        if (setDefault)
        {
            Instantiate(homebase, homebase.transform.position, homebase.transform.rotation);
        }
        else
        {
            Vector3 pos = new Vector3(xCoord, yCoord, zCoord);
            Quaternion rot = Quaternion.Euler(xRot, yRot, zRot);
            Instantiate(homebase, pos, rot);
        }
    }
}
