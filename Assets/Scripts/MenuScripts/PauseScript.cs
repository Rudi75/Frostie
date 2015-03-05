using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
  {
    toggleEnabled();
	}
	
	// Update is called once per frame
	void Update ()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (Time.timeScale == 1)
      {
        Time.timeScale = 0;
        toggleEnabled();
      }
      else
      {
        Time.timeScale = 1;
        toggleEnabled();
      }
    }
	}

  void toggleEnabled()
  {
      foreach (Transform child in transform)
      {
          child.gameObject.SetActive(!child.gameObject.activeSelf);
      }
  }
}
