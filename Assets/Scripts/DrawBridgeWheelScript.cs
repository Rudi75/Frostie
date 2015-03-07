using UnityEngine;
using System.Collections;

public class DrawBridgeWheelScript : TargetActionScript {

    private FrostieStatus frostieState;
    private SpeechBubble frostieSpeechBubble;
    private bool wheelInUse = false;

    private DrawBridgePlanksScript PlanksReference;
    public Transform plankTransform;
    public bool wheelEnabled = true;

    public KeyInterpreter keyInterpreter;
    public Camera drawBridgeWheelPopupCamera;


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

        if (frostieState != null && frostieSpeechBubble!=null && frostieState.canTurnWheel())
        {           
            if (turnWheelPressed)
            {
                if (wheelInUse)
                {
                    if (drawBridgeWheelPopupCamera != null)
                        drawBridgeWheelPopupCamera.enabled = false;
                    frostieState.isFixated=false;                    
                    wheelInUse = false;
                    frostieSpeechBubble.enabled = false;
                }
                else
                {
                    if (drawBridgeWheelPopupCamera != null)
                        drawBridgeWheelPopupCamera.enabled = true;

                    frostieState.isFixated = true;
                    
                    wheelInUse = true;
                }
            }

            if (wheelInUse)
            {
                if (wheelEnabled)
                {
                    if (PlanksReference != null)
                    {
                        if (keyInterpreter.isDrawBridgeRotateLeftKeyPressed())
                        {
                            Debug.Log("Key to the left!!");
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
                else
                {
                    frostieSpeechBubble.speech = "Hm.. looks like it is locked. Maybe I can enable it with a button or something like that.";
                    frostieSpeechBubble.speak(frostieSpeechBubble.speech);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        frostieState = other.gameObject.GetComponentInParent<FrostieStatus>();
        frostieSpeechBubble = other.gameObject.GetComponentInParent<SpeechBubble>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        frostieState = null;
    }

    protected override void performAction()
    {
        wheelEnabled = true;
    }
}
