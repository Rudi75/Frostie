using UnityEngine;
using System.Collections;

public class DrawBridgeWheelScript : TargetActionScript {

    private FrostieStatus frostieState;
    private SpeechBubble frostieSpeechBubble;
    private bool wheelInUse = false;

    private DrawBridgePlanksScript PlanksReference;
    public Transform plankTransform;
    public bool wheelEnabled = false;

    private KeyInterpreter keyInterpreter;
    public Camera drawBridgeWheelPopupCamera;


	// Use this for initialization
	void Start () {
        base.Start();
        PlanksReference = plankTransform.GetComponent<DrawBridgePlanksScript>();

        if (PlanksReference == null)
            Debug.Log("No Planks Found!");

        if (Buttons.Length == 0)
        {
            Debug.Log("Wheel is enabled cause there are no buttons!");
            wheelEnabled = true;
        }
	}
	
	// Update is called once per frame
    void Update()
    {

        Animator animator = GetComponentInParent<Animator>();
        if (frostieState != null && frostieSpeechBubble!=null && frostieState.canTurnWheel() && keyInterpreter!=null)
        {
            bool turnWheelPressed = keyInterpreter.isTurnWheelKeyPressed();

           // Debug.Log("Everything initialized!");
            if (turnWheelPressed)
            {
                Debug.Log("turnWheelPressed pressed!");

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
        Debug.Log("Collider Reached!!");
        frostieState = other.gameObject.GetComponentInParent<FrostieStatus>();
        frostieSpeechBubble = other.gameObject.GetComponentInParent<SpeechBubble>();
        keyInterpreter = other.transform.parent.gameObject.GetComponentInParent<KeyInterpreter>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        frostieState = null;
        frostieSpeechBubble = null;
        keyInterpreter = null;
    }

    protected override void performAction()
    {
        wheelEnabled = true;
    }
}
