using UnityEngine;
using System.Collections;

public class ParallaxScrollingScript : MonoBehaviour
{
  public Vector2 speed = new Vector2(1, 1);
  private Vector2 direction = new Vector2(0, 0);

  //public Transform Camera;

  private Vector3 minPosition, maxPosition;
  private float x,y;

  private int skipScrolling;

	// Use this for initialization
	void Start ()
  {
    x = Camera.main.transform.position.x;
    y = Camera.main.transform.position.y;
    skipScrolling = 2;
	}
	
	// Update is called once per frame
	void Update ()
  {
    if(skipScrolling > 0)
    {
      skipScrolling--;
      x = Camera.main.transform.position.x;
      y = Camera.main.transform.position.y;
      return;
    }

    var oldCamX = x;
    var oldCamY = y;

    x = Camera.main.transform.position.x;
    y = Camera.main.transform.position.y;

    Vector3 movement = new Vector3(
      speed.x * (oldCamX - x),
      speed.y * (oldCamY - y),
      0);

    movement.x -= oldCamX - x;
    movement.y -= oldCamY - y;
    
    transform.Translate(movement);
	}
}
