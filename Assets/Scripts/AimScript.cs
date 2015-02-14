using UnityEngine;
using System.Collections;

public class AimScript : MonoBehaviour {

    private int state;
    private Transform arrow;
    private Transform halfCircle;
    private int direction = 1;
    private float angle;
    private Vector3 scale;

    public float rotationPerFrame = 3;
    public float sizeChangePerFrame = 0.05f;
	// Use this for initialization
	void Start () {
        state = 0;
        foreach (Transform child in transform)
        {
            if(child.name.Contains("Arrow"))
            {
                arrow = child;
            }
            else
            {
                halfCircle = child;
            }
            angle = arrow.localEulerAngles.z;
        }
        arrow.GetComponent<SpriteRenderer>().enabled = false;
        halfCircle.GetComponent<SpriteRenderer>().enabled = false;
        scale = arrow.localScale;
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	switch(state)
    {
        case 0:
            {
                arrow.GetComponent<SpriteRenderer>().enabled = false;
                halfCircle.GetComponent<SpriteRenderer>().enabled = false;

               if(Input.GetKeyDown(KeyCode.LeftShift))
               { 
                   state = 1;
               }
               break;
            }
        case 1:
            {
                arrow.GetComponent<SpriteRenderer>().enabled = true;
                halfCircle.GetComponent<SpriteRenderer>().enabled = true;
                arrow.localScale = new Vector3(1, 1, 1);
                arrow.Rotate(0, 0,direction * rotationPerFrame);
                angle += direction * rotationPerFrame;
                if(angle > 90 || angle < -90)
                {
                    direction *= -1;
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    state = 2;
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    state = 0;
                }
                break;
            }
        case 2:
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    state = 1;
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    state = 0;
                }
                scale += new Vector3(sizeChangePerFrame * direction, sizeChangePerFrame * direction,0);
                if(scale.x > 1 || scale.x < 0.1)
                {
                    direction *= -1;
                }
                arrow.localScale = scale;
                break;
            }
    }
	}
}
