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
    public GameObject throwingObject;


    bool isThrown = false;
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
                angle = 0;
                scale = new Vector3(1, 1, 1);
                arrow.GetComponent<SpriteRenderer>().enabled = false;
                halfCircle.GetComponent<SpriteRenderer>().enabled = false;

               if(Input.GetKeyDown(KeyCode.LeftShift) && !isThrown)
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
                angle += direction * rotationPerFrame;
                arrow.localEulerAngles = new Vector3(0, 0, angle);
                if(angle > 90 || angle < -90)
                {
                    direction *= -1;
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    FrostieScript frostie = throwingObject.GetComponentInParent<FrostieScript>();
                    if(frostie != null && frostie.viewDirection < 0)
                    {
                        angle *= -1;
                    }
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
                    throwingObject.AddComponent<Rigidbody2D>();
                    throwingObject.rigidbody2D.fixedAngle = true;
                    float xValue = angle / -90;
                    float yValue =  1 - Mathf.Abs(angle / 90);

                    Vector2 throwVector = new Vector2(xValue / Mathf.Max(xValue, yValue), yValue / Mathf.Max(xValue, yValue));
                    throwingObject.rigidbody2D.AddForce(throwVector * 800 * scale.x);
                    isThrown = true;

                    FrostieScript frostie = throwingObject.GetComponentInParent<FrostieScript>();
                    if(frostie != null)
                    {
                        frostie.isHeadOff = true;
                    }
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
    public void reset()
    {
        isThrown = false;
    }
}
