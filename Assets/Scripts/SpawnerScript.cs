using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

    public GameObject objectToSpawnPrefab;
    private GameObject thingy;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (thingy == null)
            thingy = Instantiate(objectToSpawnPrefab, transform.position, Quaternion.identity) as GameObject;
	}
}
