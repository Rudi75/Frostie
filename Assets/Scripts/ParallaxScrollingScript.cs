﻿using UnityEngine;
using System.Collections;

public class ParallaxScrollingScript : MonoBehaviour
{
  public Vector2 speed = new Vector2(1, 1);
  private Vector2 direction = new Vector2(0, 0);

  public Transform Player;
  public Transform Camera;
  public Vector2 Margin;
  public Vector2 Smoothing;

  public BoxCollider2D Bounds;

  private Vector3 minPosition, maxPosition;
  private float x,y;

  public bool IsFollowing = true;

	// Use this for initialization
	void Start ()
  {
    minPosition = Bounds.bounds.min;
    maxPosition = Bounds.bounds.max;

    x = Camera.position.x;
    y = Camera.position.y;
	}
	
	// Update is called once per frame
	void Update ()
  {

    var oldCamX = x;
    var oldCamY = y;

    x = Camera.position.x;
    y = Camera.position.y;

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
      direction.x = 0;
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


    movement *= Time.deltaTime;
    transform.Translate(movement);
    //camera.transform.position = new Vector3(x, y, transform.position.z);
	}
}
