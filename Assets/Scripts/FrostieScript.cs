using UnityEngine;
using System.Collections;

public class FrostieScript : MonoBehaviour {
	
	public float speed = 10;
	public float jumpHight = 5;
	private bool letJump;
	public bool letGoRight = true;
	public bool letGoLeft = true;
	

	private float movement;
	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log ("collision");


		  
		if (collision.collider.tag == "surfaceTop") {
			letJump = true;
			Debug.Log ("top");
		 }

		if (collision.collider.tag == "surfaceLeft") {
			Debug.Log ("left");
			letGoRight = false;
		}

		if (collision.collider.tag == "surfaceRight") {
			Debug.Log ("right");
			letGoLeft = false;
		}
	}


	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.tag == "surfaceLeft") {
			Debug.Log ("collisionExit left");
			letGoRight = true;
		}
		
		if (collision.collider.tag == "surfaceRight") {
			Debug.Log ("collisionExit right");
			letGoLeft = true;
		}
	}




	void Update()
	{

		float inputX = Input.GetAxis("Horizontal");

		if ((inputX < 0 && !letGoLeft) || (inputX > 0 && !letGoRight)) {
			inputX = 0;
				}


		movement =speed * inputX;

		if (Input.GetKeyDown ("space") && letJump){
			letJump = false;
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
