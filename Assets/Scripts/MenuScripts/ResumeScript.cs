using UnityEngine;
using System.Collections;

public class ResumeScript : MonoBehaviour 
{

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

    public void OnAction()
    {
        Time.timeScale = 1;
        var pauseScript = GetComponentInParent<PauseScript>();
        pauseScript.toggleEnabled();
    }
}
