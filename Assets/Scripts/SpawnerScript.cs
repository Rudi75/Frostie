using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class SpawnerScript : TargetActionScript 
{
    public bool OnAction = false;
    public bool destroyOnAction = false;
    public GameObject objectToSpawnPrefab;
    private GameObject thingy;
	
	// Update is called once per frame
	void Update () {
        if (thingy == null && !OnAction)
            thingy = Instantiate(objectToSpawnPrefab, transform.position, Quaternion.identity) as GameObject;
	}

    override protected void performAction()
    {
        if (thingy != null && destroyOnAction)
        {
            Destroy(thingy);
            thingy = null;
        }

        if (thingy == null && OnAction)
            thingy = Instantiate(objectToSpawnPrefab, transform.position, Quaternion.identity) as GameObject;
    }

    public override void saveData(SavedDataContainer dataContainer)
    {
       
    }

    public override void loadData(SavedDataContainer dataContainer)
    {
    }
}
