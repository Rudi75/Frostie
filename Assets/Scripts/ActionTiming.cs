using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class ActionTiming : MonoBehaviour 
{
    private List<Animator> animators;

    public float TimeOfInvisibility;
    public float TimeOfVisibility;

    private float deltaTime;

    // Use this for initialization
    void Start()
    {
        animators = new List<Animator>(GetComponentsInChildren<Animator>());
    }

	
	// Update is called once per frame
	void Update () 
    {
        deltaTime -= Time.deltaTime;
        if (deltaTime < 0)
        {
            foreach (var animator in animators)
            {
                bool actionState = !animator.GetBool("Action");
                deltaTime = actionState ? TimeOfVisibility : TimeOfInvisibility;
                animator.SetBool("Action", actionState);
            }
        }
	}
}
