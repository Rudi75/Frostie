using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	public bool canBeMoved;
	public bool getsPushed;
	public bool isStuckLeft;
	public bool isStuckRight;

	public float speedReduction = 1f;

	private Vector2 movement = new Vector2(0,0);
	  
	void OnCollisionEnter2D(Collision2D collision)
	{
		string otherObject = collision.gameObject.tag;
		string thisCollider = collision.contacts[0].otherCollider.tag;

		if (otherObject == "Block") 
		{
			string otherCollider = collision.collider.tag;
			if(otherCollider == "surfaceLeft")
			{
				isStuckRight = true;
			}
			if(otherCollider == "surfaceRight")
			{
				isStuckLeft = true;
			}
		}

		Debug.Log("Block : Collision");

		if (otherObject == "Player") 
		{
			Debug.Log("Block : Player Collided with " + thisCollider);
			FrostieScript frostieScript = collision.gameObject.GetComponent<FrostieScript>();

			if(frostieScript.isGrounded())
			{
				float speed = frostieScript.speed;

				if(thisCollider == "surfaceRight")
				{
					movement.x = -1 * speed;
				}
				if(thisCollider == "surfaceLeft")
				{
					movement.x = speed;
				}
			}
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		string otherObject = collision.gameObject.tag;
		string thisCollider = collision.contacts[0].otherCollider.tag;
		if (otherObject == "Block") 
		{
			string otherCollider = collision.collider.tag;
			if(otherCollider == "surfaceLeft")
			{
				isStuckRight = false;
			}
			if(otherCollider == "surfaceRight")
			{
				isStuckLeft = false;
			}
		}
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
		if(canBeMoved)
		{
			rigidbody2D.velocity = movement;
		}
	}
}