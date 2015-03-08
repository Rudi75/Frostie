using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utils;

public class PlayerFollowingScript : SaveableScript 
{
    public Transform Player;
    public List<string> PartNames;
    public List<float> DistanceToTrigger;

    private Animator compAnimator;
    private bool isPlaying = false;

	// Use this for initialization
	void Start () 
    {
        compAnimator = GetComponent<Animator>();
        if(compAnimator == null) compAnimator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 playerPos = Player.transform.position;
        Vector3 pos = transform.position;

        float curDis = Vector3.Distance(playerPos, pos);
        float dis = DistanceToTrigger.FirstOrDefault();
        if(Mathf.Abs(curDis) < dis)
        {
            if (compAnimator != null && !isPlaying)
            {
                isPlaying = true;
                string name = PartNames.FirstOrDefault();
                compAnimator.Play(name);
                PartNames.Remove(name);
                DistanceToTrigger.Remove(dis);
            }
        }
	}

    public void AnimationEndCallback()
    {
        isPlaying = false;
    }

    public override void saveData(SavedDataContainer dataContainer)
    {
        dataContainer.AddData("Pos", transform.position);
        dataContainer.AddData("Names", PartNames);
        dataContainer.AddData("Dis", DistanceToTrigger);
        dataContainer.AddData("isP", isPlaying);
    }

    public override void loadData(SavedDataContainer dataContainer)
    {
        transform.position = (Vector3)dataContainer.retrieveData("Pos");
        PartNames = dataContainer.retrieveData("Names") as List<string>;
        DistanceToTrigger = dataContainer.retrieveData("Dis") as List<float>;
        isPlaying = (bool)dataContainer.retrieveData("isP");
    }
}
