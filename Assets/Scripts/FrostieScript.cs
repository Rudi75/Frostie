using UnityEngine;
using System.Collections;

public class FrostieScript : MonoBehaviour {
	
	public float speed = 10;
	public float jumpHight = 5;
	public bool letGoRight = true;
	public bool letGoLeft = true;
	

	private float movement;
	private Vector3 bottomCenter;

	public bool isGrounded()
	{
		float distToGround = 0;
		float distToFront = 0;
		foreach (Transform child in transform)
		{
			if(child.name == "FrostieBottom")
			{
				distToGround = child.collider2D.bounds.extents.y + 0.1f;
				distToFront = child.collider2D.bounds.extents.x + 0.1f;
				bottomCenter = child.position;
			}
		}
		RaycastHit2D hitMiddle =  Physics2D.Raycast(bottomCenter,-Vector2.up,distToGround);
		RaycastHit2D hitFront =  Physics2D.Raycast(bottomCenter + new Vector3(distToFront,0,0),-Vector2.up,distToGround);
		RaycastHit2D hitBack =  Physics2D.Raycast(bottomCenter - new Vector3(distToFront,0,0),-Vector2.up,distToGround);
		if (hitMiddle.collider == null && hitFront.collider == null && hitBack.collider == null)
		{
			return false;
		}
		else
		{
			return true;
		}

				
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "surfaceLeft") {
			letGoRight = false;
		}

		if (collision.collider.tag == "surfaceRight") {
			letGoLeft = false;
		}
	}


	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.tag == "surfaceLeft") {
			letGoRight = true;
		}
		
		if (collision.collider.tag == "surfaceRight") {
			letGoLeft = true;
		}

	}




	void Update()
	{

		float inputX = Input.GetAxis("Horizontal");

		if ((inputX < 0 && !letGoLeft) || (inputX > 0 && !letGoRight)) {
			inputX = 0;
				}


		movement = speed * inputX;

		if (Input.GetKeyDown ("space") && isGrounded()){
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
