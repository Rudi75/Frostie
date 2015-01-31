using UnityEngine;
using System.Collections;
using System.Linq;

public abstract class TargetActionScript : MonoBehaviour {

    public Transform[] Buttons;

    abstract protected void performAction();

    void Awake()
    {
        foreach (Transform button in Buttons)
        {
             ButtonScript buttonScript = button.GetComponent<ButtonScript>();
             buttonScript.setTarget(this);
        }
    }

    public void notify(Transform _button)
    {
        if (!Buttons.Contains(_button))
            Debug.LogWarning("Warning: Button not in Array of target!");


        foreach (Transform button in Buttons)// all buttons pressed?
	    { 
            ButtonScript buttonScript = button.GetComponent<ButtonScript>();
            if (!buttonScript.isPressed)
                return;
	    }

        performAction();
    }
}
