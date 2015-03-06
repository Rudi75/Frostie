using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class ActionTiming : MonoBehaviour 
{
    private Animator animator;

    public bool PlaySoundOnVisible = false;
    public float TimeOfInvisibility;
    public float TimeOfVisibility;

    private float deltaTime;
    private bool actionState = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

	
	// Update is called once per frame
	void Update () 
    {
        deltaTime -= Time.deltaTime;
        if (deltaTime < 0)
        {
            actionState = actionState ? false: true;
            deltaTime = actionState ? TimeOfVisibility : TimeOfInvisibility;
            animator.SetBool("Action", actionState);
            if (PlaySoundOnVisible)
            { 
                var audioSrc = GetComponent<AudioSource>();
                audioSrc.enabled = actionState;
            }
        }
	}
}
