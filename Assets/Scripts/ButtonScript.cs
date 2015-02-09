using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class ButtonScript : MonoBehaviour {

	private Animator animator;

	public bool isPressed;
	public bool withRelease = true;
    private TargetActionScript target;
    private Edges buttonEdge;

	void Awake()
	{
		animator = GetComponentInParent<Animator>();
		isPressed = false;

        float rotation = transform.localEulerAngles.z;

        if (rotation >= 225 && rotation < 315)
        {
            buttonEdge = Edges.RIGHT;
        }
        else if (rotation >= 45 && rotation < 135)
        {
            buttonEdge = Edges.LEFT;
        }
        else if (rotation >= 135 && rotation < 225)
        {
            buttonEdge = Edges.BOTTOM;
        }
        else
        {
            buttonEdge = Edges.TOP;
        }
	}
    

	public void OnCollisionEnter2D(Collision2D collision)
	{



		    Edges collisionEdge = CollisionHelper.getCollisionEdge(collision);
            Debug.Log("Collision : " + buttonEdge + " : " + collisionEdge);

		    if(buttonEdge.Equals(collisionEdge))//Button pressed
		    {
			    //FrostiePartScript part = collision.collider.gameObject.GetComponent<FrostiePartScript>();
			    //TODO restriction ButtonSize and FrostiePart

			    isPressed = true;
			    animator.SetBool("pressed",isPressed);
                target.notify(this.transform);
		    }

	}

    public void setTarget(TargetActionScript target)
    {
        this.target = target;
    }

	void FixedUpdate()
	{
		if(withRelease && isPressed)
		{

            if (CollisionHelper.getCollidingObject(getButtonCollider(), buttonEdge, 0.1f) == null)//Button released
			{
				
				isPressed = false;
				animator.SetBool("pressed",isPressed);
                target.notify(this.transform);
			}
		}
	}
    private Collider2D getButtonCollider()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Button")
            {
                return child.collider2D;
            }
        }
        return null;
    }
}
