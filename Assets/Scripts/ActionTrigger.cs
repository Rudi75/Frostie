using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ActionTrigger : MonoBehaviour 
{
    private List<Animator> animators;
	// Use this for initialization
	void Start () 
    {
        animators = new List<Animator>(GetComponentsInChildren<Animator>());
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        FrostieScript frostie = other.GetComponentInParent<FrostieScript>();
        if ((frostie != null) && animators.Any())
        {
            Debug.Log("enter");
            foreach (var animator in animators)
            {
                animator.SetBool("Action", true);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        FrostieScript frostie = other.GetComponentInParent<FrostieScript>();
        if ((frostie != null) && animators.Any())
        {
            Debug.Log("exit");
            foreach (var animator in animators)
            {
                animator.SetBool("Action", false);
            }
        }
    }
}
