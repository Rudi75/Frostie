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

    public void Awake()
    {
        base.Awake();
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
                compAnimator.Play(Animator.StringToHash(name), 0, 0);
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
        dataContainer.AddData("Names", PartNames);
        dataContainer.AddData("Dis", DistanceToTrigger);
        dataContainer.AddData("isP", isPlaying);

        AnimatorStateInfo currentState = compAnimator.GetCurrentAnimatorStateInfo(0);
        float playbackTime = currentState.normalizedTime % 1;
        dataContainer.AddData("AnimName", currentState.nameHash); 
        dataContainer.AddData("AnimTime", playbackTime);     
    }

    public override void loadData(SavedDataContainer dataContainer)
    {
        PartNames = dataContainer.retrieveData("Names") as List<string>;
        DistanceToTrigger = dataContainer.retrieveData("Dis") as List<float>;
        isPlaying = (bool)dataContainer.retrieveData("isP");

        int nameHash = (int)dataContainer.retrieveData("AnimName");
        float playbackTime = (float)dataContainer.retrieveData("AnimTime");
        compAnimator.Play(nameHash, 0, playbackTime);
    }
}
