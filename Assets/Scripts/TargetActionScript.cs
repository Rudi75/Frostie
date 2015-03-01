using UnityEngine;
using System.Collections;
using System.Linq;

public abstract class TargetActionScript : MonoBehaviour {

    public Transform[] Buttons;

    abstract protected void performAction();

    public void Start()
    {
        Debug.Log("Start");
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


        foreach (Transform button in Buttons)// all buttons pressed?
	    { 
            ButtonScript buttonScript = button.GetComponent<ButtonScript>();
            if (!buttonScript.isPressed)
                return;
	    }

        performAction();
    }
}
