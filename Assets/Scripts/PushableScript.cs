using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class PushableScript : MonoBehaviour {
	
	public bool isStuckLeft;
	public bool isStuckRight;

	public float speedReduction = 1f;

	private Vector2 movement = new Vector2(0,0);
	  
	void OnCollisionEnter2D(Collision2D collision)
	{
		Edges edge = CollisionHelper.getCollisionEdge (collision);

		string otherObject = collision.gameObject.tag;

		if (otherObject == "Block") 
		{
			if (Edges.RIGHT.Equals(edge)) {
				isStuckRight = true;
			}
			
			if (Edges.LEFT.Equals(edge)) {
				isStuckLeft = true;
			}
		}

		if (otherObject == "Player") 
		{
			FrostieScript frostieScript = collision.gameObject.GetComponent<FrostieScript>();

			if(frostieScript.isGrounded())
			{
				float speed = frostieScript.speed;

				if (Edges.RIGHT.Equals(edge)) {
					movement.x = -1 * speed;
				}
				
				if (Edges.LEFT.Equals(edge)) {
					movement.x = speed;
				}
			}
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{

	}


	void Update()
	{
		if(movement.x > 0)
			movement.x -= speedReduction * Time.deltaTime;
		if(movement.x < 0)
			movement.x += speedReduction * Time.deltaTime;
	}


	void FixedUpdate()
	{
			rigidbody2D.velocity = movement;
	}
}