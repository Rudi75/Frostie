using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class PushableScript : MonoBehaviour {
	
	public bool isStuckLeft;
	public bool isStuckRight;

	public float speedReduction = 1f;

	private float movement = 0;
	  
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
					movement= -1 * speed;
				}
				
				if (Edges.LEFT.Equals(edge)) {
					movement= speed;
				}
			}
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{

	}


	void Update()
	{
		if(movement > 0)
			movement -= speedReduction * Time.deltaTime;
		if(movement < 0)
			movement += speedReduction * Time.deltaTime;
	}


	void FixedUpdate()
	{
		float speedX = movement;
		float speedY = rigidbody2D.velocity.y;
		rigidbody2D.velocity = new Vector2(speedX,speedY);
	}
}