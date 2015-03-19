using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class DrawBridgeActionScript : TargetActionScript 
{
    public DrawBridgeScript Drawbridge;
    public bool IsEnabled = true;

    private Camera PopUpCam = null;

    private Animator compAnimator;
    private FrostieStatus Frostie = null;
    private SpeechBubble Info = null;
    private KeyInterpreter KeyInput = null;
    private bool FrostieAction = false;

	// Use this for initialization
	void Start () 
    {
        compAnimator = GetComponent<Animator>();
        PopUpCam = transform.parent.GetComponentInChildren<Camera>();
        KeyInput = FindObjectOfType<KeyInterpreter>();
        Info = FindObjectOfType<SpeechBubble>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Frostie = other.GetComponentInParent<FrostieStatus>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var frostie = other.GetComponentInParent<FrostieStatus>();
        if (frostie != null)
        {
            Frostie = null;
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Frostie != null && KeyInput != null)
        {
            if (KeyInput.isTurnWheelKeyPressed() && Frostie.canTurnWheel() && IsEnabled)
            {
                FrostieAction = !FrostieAction;
                Frostie.isFixated = FrostieAction;
                if (PopUpCam != null)
                {
                    PopUpCam.enabled = FrostieAction;
                }
            }
            else if (Info != null && !IsEnabled)
            {
                string info = "Hm.. looks like it is locked. Maybe I can enable it with a button or something like that.";
                Info.speak(info);
            }
        }
        if (FrostieAction)
        {
            if (KeyInput.isDrawBridgeRotateLeftKeyPressed())
            {
                Drawbridge.DrawbridgeUp();
                compAnimator.Play(Animator.StringToHash("Left"), 0, 0);
            }
            if (KeyInput.isDrawBridgeRotateRightKeyPressed())
            {
                Drawbridge.DrawbridgeDown();
                compAnimator.Play(Animator.StringToHash("Right"), 0, 0);
            }
        }
	}

    protected override void performAction()
    {
        IsEnabled = !IsEnabled;
    }

    public override void saveData(SavedDataContainer dataContainer)
    {
        dataContainer.AddData("enabled", IsEnabled);
    }

    public override void loadData(SavedDataContainer dataContainer)
    {
        IsEnabled = (bool)dataContainer.retrieveData("enabled");
    }
}
