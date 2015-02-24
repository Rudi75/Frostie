using UnityEngine;
using System.Collections;

public class start : MonoBehaviour {

  public string level = "";

	// Use this for initialization
	void Start ()
  {
	
	}
	
	// Update is called once per frame
	void Update ()
  {
	
	}

  void OnMouseDown()
  {
    if (renderer.enabled)
    {
      LoadLevel();
    }
  }

  public void LoadLevel()
  {
    Time.timeScale = 1;
    Application.LoadLevel(level);
  }
}
