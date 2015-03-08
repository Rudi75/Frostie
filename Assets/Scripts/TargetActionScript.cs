using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public abstract class TargetActionScript : MonoBehaviour {

    public Transform[] Buttons;

    public bool onlyActiveWhenPressed = false;
    private bool activated_ = false;

    abstract protected void performAction();
    public List<SpeechTriggerScript> speechTriggerToActivate;
    public List<SpeechTriggerScript> speechTriggerToDeactivate;

    public void Start()
    {
        foreach (Transform button in Buttons)
        {
             ButtonScript buttonScript = button.GetComponent<ButtonScript>();
             buttonScript.addTarget(this);
        }
    }

    public void notify(Transform _button)
    {
        if (!Buttons.Contains(_button))
            Debug.LogWarning("Warning: Button not in Array of target!");

        bool allButtonspressed = true;
        foreach (Transform button in Buttons)// all buttons pressed?
	    { 
            ButtonScript buttonScript = button.GetComponent<ButtonScript>();
            if (!buttonScript.isPressed)
                allButtonspressed = false;
	    }
        if (allButtonspressed && !activated_)
        {
            performAction();
            activated_ = true;
        }
        else if (onlyActiveWhenPressed && activated_)
        {
            performAction();
            activated_ = false;
        }

        
    }
}
