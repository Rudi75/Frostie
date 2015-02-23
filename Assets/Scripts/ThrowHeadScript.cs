using UnityEngine;
using System.Collections;

public class ThrowHeadScript : MonoBehaviour {

    private int state;
    private Transform arrow;
    private Transform halfCircle;
    private int direction = 1;
    private float angle;
    private Vector3 scale;
    private bool forward = false;
    private bool backward = false;

    public float rotationPerFrame = 3;
    public float sizeChangePerFrame = 0.05f;

    public int throwForce = 19;
   

    private FrostiePartManager partManager;


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

        Transform frostieAnimated = transform;
        while(frostieAnimated.parent != null && !frostieAnimated.name.Contains("Animated"))
        {
            frostieAnimated = frostieAnimated.parent;
        }

        partManager = frostieAnimated.GetComponentInChildren<FrostiePartManager>();
        
	}
	
	// Update is called once per frame
    void LateUpdate()
    {
        
	switch(state)
    {
        case 0:
            {
                angle = 0;
                scale = new Vector3(1, 1, 1);
                arrow.GetComponent<SpriteRenderer>().enabled = false;
                halfCircle.GetComponent<SpriteRenderer>().enabled = false;

               if(forward && partManager.getHeadClone() == null)
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
                if(angle > 70 || angle < -70)
                {
                    direction *= -1;
                }
                if (forward)
                {
                   FrostieMoveScript moveScript = partManager.getActivePart().GetComponent<FrostieMoveScript>();
                    if(moveScript.viewDirection < 0)
                    {
                        angle *= -1;
                    }
                    state = 2;
                }
                if (backward)
                {
                    state = 0;
                }
                break;
            }
        case 2:
            {
                if (backward)
                {
                    state = 1;
                }
                if (forward)
                {

                    GameObject headClone = partManager.decoupleHead();

                    float xValue = angle / -90;
                    float yValue =  1 - Mathf.Abs(angle / 90);

                    Vector2 throwVector = new Vector2(xValue / Mathf.Max(xValue, yValue), yValue / Mathf.Max(xValue, yValue));

                    headClone.rigidbody2D.AddForce(throwVector * throwForce * scale.x, ForceMode2D.Impulse);
                
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
    forward = false;
    backward = false;
	}
    public void setForward()
    {
        forward = true;
    }
    public void setBackward()
    {
        backward = true;
    }
}
