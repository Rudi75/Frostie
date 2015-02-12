using UnityEngine;
using System.Collections;

public class ParallaxScrollingScript : MonoBehaviour
{
  public Vector2 speed = new Vector2(1, 1);
  private Vector2 direction = new Vector2(0, 0);

  public Transform Player;
  public Vector2 Margin;
  public Vector2 Smoothing;

  public BoxCollider2D Bounds;

  private Vector3 minPosition, maxPosition;

  public bool IsFollowing = true;

	// Use this for initialization
	void Start ()
  {
    minPosition = Bounds.bounds.min;
    maxPosition = Bounds.bounds.max;

    
	}
	
	// Update is called once per frame
	void Update ()
  {
    var x = camera.transform.position.x;
    var y = camera.transform.position.y;
    var oldCamX = x;
    var oldCamY = y;

    if (IsFollowing && Player != null)
    {
      if (Mathf.Abs(x - Player.position.x) > Margin.x)
        x = Mathf.Lerp(x, Player.position.x, Smoothing.x * Time.deltaTime);

      if (Mathf.Abs(y - Player.position.y) > Margin.y)
        y = Mathf.Lerp(y, Player.position.y, Smoothing.y * Time.deltaTime);
    }

    var cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / Screen.height);

    x = Mathf.Clamp(x, minPosition.x + cameraHalfWidth, maxPosition.x - cameraHalfWidth);
    y = Mathf.Clamp(y, minPosition.y + camera.orthographicSize, maxPosition.y - camera.orthographicSize);

    if (oldCamX > x)
    {
      direction.x = -1;
    }
    else if (oldCamX < x)
    {
      direction.x = 1;
    }
    else
    {
      direction.x = 1;
    }

    if (oldCamY > y)
    {
      direction.y = -1;
    }
    else if (oldCamY < y)
    {
      direction.y = 1;
    }
    else
    {
      direction.y = 0;
    }

    
    Vector3 movement = new Vector3(
      speed.x * direction.x,
      speed.y * direction.y,
      0);


    //movement *= Time.deltaTime;
    transform.Translate(movement);
    //camera.transform.position = new Vector3(x, y, transform.position.z);
	}
}
