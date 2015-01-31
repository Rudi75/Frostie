using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class ButtonScript : MonoBehaviour {

	private Animator animator;

	public bool isPressed;
	public bool withRelease = true;
    private TargetActionScript target;

	void Awake()
	{
		animator = GetComponentInParent<Animator>();
		isPressed = false;
	}
    

	public void OnCollisionEnter2D(Collision2D collision)
	{
        
        Debug.Log("Button Collision");



		    Edges edge = CollisionHelper.getCollisionEdge(collision);

		    if(Edges.TOP.Equals(edge))//Button pressed
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
			Debug.Log("Button FixedUpdate");
			if(!CollisionHelper.isCollision(collider2D,Edges.TOP))//Button pressed
			{
				//FrostiePartScript part = collision.collider.gameObject.GetComponent<FrostiePartScript>();
				//TODO restriction ButtonSize and FrostiePart
				
				isPressed = false;
				animator.SetBool("pressed",isPressed);
                target.notify(this.transform);
			}
		}
	}

}
