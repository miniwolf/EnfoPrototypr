using UnityEngine;
using System.Collections;

public class ObjectPlacement : MonoBehaviour {

    public GameObject obj;

    // Default base position and rotation
    public bool setDefault = true;

    // Position and rotation parameters
    public float xCoord, yCoord, zCoord;
    public float xRot, yRot, zRot;

    void Start () {
        // Spawns the object instantly
        Invoke("SpawnObject", 0);
    }

    void SpawnObject()
    {
        if (setDefault)
        {
            Instantiate(obj, obj.transform.position, obj.transform.rotation);
        }
        else
        {
            Vector3 pos = new Vector3(xCoord, yCoord, zCoord);
            Quaternion rot = Quaternion.Euler(xRot, yRot, zRot);
            Instantiate(obj, pos, rot);
        }
    }
}