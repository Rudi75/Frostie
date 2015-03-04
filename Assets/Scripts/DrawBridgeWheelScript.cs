using UnityEngine;
using System.Collections;

public class DrawBridgeWheelScript : TargetActionScript {

    private FrostieStatus frostie;
    private bool wheelInUse = false;

    private DrawBridgePlanksScript PlanksReference;
    public Transform plankTransform;
    public bool wheelEnabled = true;

    public KeyInterpreter keyInterpreter;


	// Use this for initialization
	void Start () {
        base.Start();
        PlanksReference = plankTransform.GetComponent<DrawBridgePlanksScript>();

        if (PlanksReference == null)
            Debug.Log("No Planks Found!");
	}
	
	// Update is called once per frame
    void Update()
    {
        if (keyInterpreter == null)
            Debug.Log("KeyInterpreter in Wheel setzen!!");

        bool turnWheelPressed = keyInterpreter.isTurnWheelKeyPressed();
        Animator animator = GetComponentInParent<Animator>();

        if (frostie != null && frostie.canTurnWheel())
        {

            if (turnWheelPressed)
            {
                if (wheelInUse)
                {                    
                    frostie.isFixated=false;
                    
                    wheelInUse = false;
                }
                else
                {
                    frostie.isFixated = true;
                    
                    wheelInUse = true;
                }
            }

            if (wheelInUse && wheelEnabled)
            {
                if (PlanksReference != null)
                {
                    if (keyInterpreter.isDrawBridgeRotateLeftKeyPressed())
                    {
                        if (!PlanksReference.IsBridgeLockedInDirection(DrawBridgePlanksScript.Directions.left))
                            animator.Play("DrawingbridgewheeleAnimationLeft");

                        PlanksReference.rotateOnce(DrawBridgePlanksScript.Directions.left);
                    }

                    if (keyInterpreter.isDrawBridgeRotateRightKeyPressed())
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
        frostie = other.gameObject.GetComponentInParent<FrostieStatus>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        frostie = null;
    }

    protected override void performAction()
    {
        wheelEnabled = true;
    }
}
