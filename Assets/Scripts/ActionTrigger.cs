using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ActionTrigger : MonoBehaviour 
{
    public bool PlaySoundOnTriggered = false;
    private Animator animator;
	// Use this for initialization
	void Start () 
    {    
        foreach (Transform item in transform)
        {
            animator = item.GetComponent<Animator>();
            if (animator != null)
                break;
        }
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        FrostieStatus frostie = other.GetComponentInParent<FrostieStatus>();
        if (frostie != null)
        {
            animator.SetBool("Action", true);
            if (PlaySoundOnTriggered)
            {
                var audioSrc = GetComponent<AudioSource>();
                audioSrc.enabled = true;
            }
        }
    }

    //public void OnTriggerExit2D(Collider2D other)
    //{
    //    FrostieStatus frostie = other.GetComponentInParent<FrostieStatus>();
    //    if (frostie != null)
    //    {
    //         animator.SetBool("Action", false);
    //    }
    //}
}
