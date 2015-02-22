using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts.Utils;

public class ButtonScript : MonoBehaviour {

	private Animator animator;

	public bool isPressed;
	public bool withRelease = true;
    private TargetActionScript target;
    private Enums.Edges  buttonEdge;

	void Awake()
	{
		animator = GetComponentInParent<Animator>();
		isPressed = false;

        float rotation = transform.localEulerAngles.z;

        if (rotation >= 225 && rotation < 315)
        {
            buttonEdge = Enums.Edges.RIGHT;
        }
        else if (rotation >= 45 && rotation < 135)
        {
            buttonEdge = Enums.Edges.LEFT;
        }
        else if (rotation >= 135 && rotation < 225)
        {
            buttonEdge = Enums.Edges.BOTTOM;
        }
        else
        {
            buttonEdge = Enums.Edges.TOP;
        }
	}
    

	public void OnCollisionEnter2D(Collision2D collision)
	{



        Enums.Edges collisionEdge = CollisionHelper.getCollisionEdge(collision);

            GameObject other = collision.collider.gameObject;

		    if(buttonEdge.Equals(collisionEdge) && !other.tag.Contains("Lethal"))//Button pressed by everythin except enemies
		    {
                if(transform.name.Contains("Small"))
                {
                    press();
                
                }else if(transform.name.Contains("Medium"))
                {
                    if(!other.name.Contains("Head"))
                    {
                        press();
                    }
                }else
                {
                    if(!other.name.Contains("Head") && !other.name.Contains("Middle"))
                    {
                        press();
                    }
                }
		    }

	}
    private void press()
    {
        isPressed = true;
        animator.SetBool("pressed", isPressed);
        target.notify(this.transform);
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
