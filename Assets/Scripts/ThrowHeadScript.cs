using UnityEngine;
using System.Collections;

public class ThrowHeadScript : MonoBehaviour {

    private int state;
    private Transform arrow;
    private Transform halfCircle;
    private int direction = 1;
    private float angle;
    private Vector3 scale;

    public float rotationPerFrame = 3;
    public float sizeChangePerFrame = 0.05f;
    public GameObject throwingObject;

    public int throwForce = 800;
    public GameObject Dummy;
    FrostieScript frostie;

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
        frostie = throwingObject.GetComponentInParent<FrostieScript>();
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

                    Vector3 dummyPosition = throwingObject.transform.position - throwingObject.transform.localPosition;
                    GameObject dummyObject = Instantiate(Dummy,dummyPosition , Quaternion.identity) as GameObject;

                    dummyObject.transform.parent = throwingObject.transform.parent.parent.parent.parent;
                    throwingObject.transform.parent = dummyObject.transform;

                    dummyObject.AddComponent<Rigidbody2D>();
                    dummyObject.rigidbody2D.fixedAngle = true;
                    float xValue = angle / -90;
                    float yValue =  1 - Mathf.Abs(angle / 90);

                    Vector2 throwVector = new Vector2(xValue / Mathf.Max(xValue, yValue), yValue / Mathf.Max(xValue, yValue));

                   dummyObject.rigidbody2D.AddForce(throwVector * throwForce * scale.x);
                    isThrown = true;

                    
                    if(frostie != null)
                    {
                        frostie.isPartMising = true;
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
