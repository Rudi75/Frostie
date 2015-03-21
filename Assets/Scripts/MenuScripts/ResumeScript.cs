using UnityEngine;
using System.Collections;

public class ResumeScript : MonoBehaviour 
{
    public void OnAction()
    {
        Time.timeScale = 1;
        var pauseScript = GetComponentInParent<PauseScript>();
        pauseScript.toggleEnabled();
    }
}
