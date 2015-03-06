using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts.Utils;
using System.Collections.Generic;

public class ButtonScript : MonoBehaviour {

	private Animator animator;

	public bool isPressed;
	public bool withRelease = true;
    public List<TargetActionScript> targets;
    private Enums.Edges  buttonEdge;

	void Awake()
	{
        targets = new List<TargetActionScript>();
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
        if (!isPressed)
        {
            isPressed = true;
            animator.SetBool("pressed", isPressed);
            audio.Play();
            foreach (TargetActionScript target in targets)
            {
                target.notify(this.transform);
            }
        }
    }

    public void addTarget(TargetActionScript target)
    {
       targets.Add(target);
    }

	void FixedUpdate()
	{
		if(withRelease && isPressed)
		{

            if (!CollisionHelper.isCollision(getButtonCollider(), buttonEdge, 0.1f))//Button released
			{
				
				isPressed = false;
				animator.SetBool("pressed",isPressed);
                foreach (TargetActionScript target in targets)
                {
                    target.notify(this.transform);
                }
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
