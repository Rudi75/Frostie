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
    LoadLevel();
  }

  public void LoadLevel()
  {
    Application.LoadLevel(level);
  }
}
