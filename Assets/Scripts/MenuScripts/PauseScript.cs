using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
  {
    Vector3 pos = new Vector3();
    pos.x = Camera.main.transform.position.x;
    pos.y = Camera.main.transform.position.y;
    pos.z = -5;

    transform.position = pos;
    toggleEnabled();
	}
	
	// Update is called once per frame
	void Update ()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (Time.timeScale == 1)
      {
        Vector3 pos = new Vector3();
        pos.x = Camera.main.transform.position.x;
        pos.y = Camera.main.transform.position.y;
        pos.z = -5;

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
    if (renderer.enabled)
    {
      renderer.enabled = false;
    }
    else
    {
      renderer.enabled = true;
    }

    for (int i = 0; i < transform.childCount; i++)
    {
      if (transform.GetChild(i).renderer.enabled)
      {
        transform.GetChild(i).renderer.enabled = false;
      }
      else
      {
        transform.GetChild(i).renderer.enabled = true;
      }
    }
  }
}
