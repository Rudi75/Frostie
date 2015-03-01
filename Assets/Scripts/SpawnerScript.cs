using UnityEngine;
using System.Collections;

public class SpawnerScript : TargetActionScript 
{
    public bool OnAction = false;
    public GameObject objectToSpawnPrefab;
    private GameObject thingy;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (thingy == null && !OnAction)
            thingy = Instantiate(objectToSpawnPrefab, transform.position, Quaternion.identity) as GameObject;
	}

    override protected void performAction()
    {
        if (thingy == null && OnAction)
            thingy = Instantiate(objectToSpawnPrefab, transform.position, Quaternion.identity) as GameObject;
    }
}
