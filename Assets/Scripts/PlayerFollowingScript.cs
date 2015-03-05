using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerFollowingScript : MonoBehaviour 
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
}
