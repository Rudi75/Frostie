using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class FrostieScript : MonoBehaviour {
	
	public float speed = 10;
	public float jumpHight = 5;
	public bool letGoRight = true;
	public bool letGoLeft = true;
	

	private float movement;


	private Collider2D getButtom()
	{
		foreach (Transform child in transform)
		{
			if(child.name == "FrostieBottom")
			{
				return child.collider2D;
			}
		}
		return null;
	}



	void OnCollisionEnter2D(Collision2D collision)
	{
		PushableScript pushScript = collision.gameObject.GetComponent<PushableScript> ();

		if(pushScript == null || (pushScript != null && !CollisionHelper.isCollision(getButtom(),Edges.BOTTOM)))
		{
			Edges edge = CollisionHelper.getCollisionEdge (collision);
			if (Edges.RIGHT.Equals(edge)) {
				letGoRight = false;
			}

			if (Edges.LEFT.Equals(edge)) {
				letGoLeft = false;
			}
		}
	}

	public bool canJump()
	{
		return CollisionHelper.isCollision(getButtom(),Edges.BOTTOM);
	}


	void Update()
	{

		float inputX = Input.GetAxis("Horizontal");

		if(!letGoLeft)
		{
			letGoLeft = !CollisionHelper.isCollision(getButtom(),Edges.LEFT);
		}

		if(!letGoRight)
		{
			letGoRight = !CollisionHelper.isCollision(getButtom(),Edges.RIGHT);
		}



		if ((inputX < 0 && !letGoLeft) || (inputX > 0 && !letGoRight)) {
			inputX = 0;
				}


		movement = speed * inputX;

		if (Input.GetKeyDown("space") && canJump()){
			rigidbody2D.AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
		} 
		
		// Make sure we are not outside the camera bounds
		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
			).y;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
			transform.position.z
			);
		
	}
	
	void FixedUpdate()
	{
		float speedX = movement;
		float speedY = rigidbody2D.velocity.y;
		rigidbody2D.velocity = new Vector2(speedX,speedY);
	}

}
