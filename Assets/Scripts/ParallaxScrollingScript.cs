using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class ParallaxScrollingScript : MonoBehaviour
{
  public Vector2 speed = new Vector2(1, 1);

  private float x,y;

  private int skipScrolling;

  public bool isLooping = false;
  private List<Transform> children;

  private float loopX;


  bool isAChildVisibleFrom(Transform parent)
  {
    bool aChildIsVisible = false;

    for (int index = 0; index < parent.childCount; index++)
    {
      Transform child = parent.GetChild(index);

      if (child.renderer != null)
      {
        aChildIsVisible |= child.renderer.IsVisibleFrom(Camera.main);

        if (aChildIsVisible)
        {
          break;
        }
      }
    }

    return aChildIsVisible;
  }


	// Use this for initialization
	void Start ()
  {
    x = Camera.main.transform.position.x;
    y = Camera.main.transform.position.y;
    skipScrolling = 2;

    if(isLooping)
    {
      children = new List<Transform>();

      for (int index = 0; index < transform.childCount; index++)
      {
        Transform child = transform.GetChild(index);

        children.Add(child);
      }

      children = children.OrderBy( t => t.position.x ).ToList();

      loopX = Mathf.Abs(children.ElementAtOrDefault(1).transform.position.x - children.ElementAtOrDefault(0).transform.position.x);
    }
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


    if(isLooping)
    {
      bool isVisible = false;
      /*
      if (children.ElementAtOrDefault(1).renderer != null)
      {
        isVisible = children.ElementAtOrDefault(1).renderer.IsVisibleFrom(Camera.main);
      }
      else
      {
        isVisible = isAChildVisibleFrom(children.ElementAtOrDefault(1));
      }
      */

      float diff = Camera.main.transform.position.x - children.ElementAtOrDefault(1).transform.position.x;

      if (Mathf.Abs(diff) <= loopX)
      {
        isVisible = true;
      }

      if (!isVisible)
      {
        Transform child = children.FirstOrDefault();
        Vector3 pos = child.transform.position;
        /*
        if (oldCamX - x > 0)
        {
          child = children.LastOrDefault();
          pos = child.transform.position;

          pos.x -= (loopX * 3.0f);
        }
        else if (oldCamX - x < 0)
        {
          child = children.FirstOrDefault();
          pos = child.transform.position;

          pos.x += loopX * 3;
        }
        */
        if (diff > 0)
        {
          child = children.FirstOrDefault();
          pos = child.transform.position;

          pos.x += (loopX * 3.0f);
        }
        else if (diff < 0)
        {
          child = children.LastOrDefault();
          pos = child.transform.position;

          pos.x -= loopX * 3;
        }

        child.transform.position = pos;

        children = children.OrderBy(t => t.position.x).ToList();

      }
    }
	}
}
