﻿using UnityEngine;
using System.Collections;

public class DrawBridgeWheelScript : TargetActionScript {

    private FrostieScript frostie;
    private bool wheelInUse = false;

    private DrawBridgePlanksScript PlanksReference;
    public Transform plankTransform;
    private bool wheelEnabled = true;

	// Use this for initialization
	void Start () {
        PlanksReference = plankTransform.GetComponent<DrawBridgePlanksScript>();

        if (PlanksReference != null)
            Debug.Log("Found Planks!");
	}
	
	// Update is called once per frame
    void Update()
    {
        Animator animator = GetComponentInParent<Animator>();

        if (frostie != null && wheelEnabled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (wheelInUse)
                {                    
                    frostie.ReleaseFixedPosition();
                    wheelInUse = false;
                }
                else
                {
                    frostie.FixatePosition();
                    wheelInUse = true;
                }
            }

            if (wheelInUse)
            {
                if (PlanksReference != null)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (!PlanksReference.IsBridgeLockedInDirection(DrawBridgePlanksScript.Directions.left))
                            animator.Play("DrawingbridgewheeleAnimationLeft");

                        PlanksReference.rotateOnce(DrawBridgePlanksScript.Directions.left);
                    }

                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (!PlanksReference.IsBridgeLockedInDirection(DrawBridgePlanksScript.Directions.right))
                            animator.Play("DrawbridgeWheelAnimationRight");

                        PlanksReference.rotateOnce(DrawBridgePlanksScript.Directions.right);
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        frostie = other.gameObject.GetComponentInParent<FrostieScript>();
    }

    protected override void performAction()
    {
        wheelEnabled = true;
    }
}
