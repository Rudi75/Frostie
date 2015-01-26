using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class FrostieScript : MonoBehaviour {
	
	public float speed = 10;
	public float jumpHight = 5;
	public bool letGoRight = true;
	public bool letGoLeft = true;
	

	private float movement;
	private Vector3 bottomCenter;
	
	public bool isCollision(Edges side)
	{
		float distToGround = 0;
		float distToFront = 0;
		foreach (Transform child in transform)
		{
			if(child.name == "FrostieBottom")
			{
				distToGround = child.collider2D.bounds.extents.y ;
				distToFront = child.collider2D.bounds.extents.x ;
				bottomCenter = child.position;
			}
		}

		RaycastHit2D hit1 = new RaycastHit2D ();
		RaycastHit2D hit2 = new RaycastHit2D ();
		RaycastHit2D hit3 = new RaycastHit2D ();

		if(Edges.BOTTOM.Equals(side))
		{
			hit1 =  Physics2D.Raycast(bottomCenter,-Vector2.up,distToGround+0.1f);
			hit2 =  Physics2D.Raycast(bottomCenter + new Vector3(distToFront-0.1f,0,0),-Vector2.up,distToGround+0.1f);
			hit3 =  Physics2D.Raycast(bottomCenter - new Vector3(distToFront-0.1f,0,0),-Vector2.up,distToGround+0.1f);
		}else if(Edges.TOP.Equals(side))
		{
			hit1 =  Physics2D.Raycast(bottomCenter,Vector2.up,distToGround+0.1f);
			hit2 =  Physics2D.Raycast(bottomCenter + new Vector3(distToFront-0.1f,0,0),Vector2.up,distToGround+0.1f);
			hit3 =  Physics2D.Raycast(bottomCenter - new Vector3(distToFront-0.1f,0,0),Vector2.up,distToGround+0.1f);
		}else if(Edges.RIGHT.Equals(side))
		{
			hit1 =  Physics2D.Raycast(bottomCenter,Vector2.right,distToFront+0.1f);
			hit2 =  Physics2D.Raycast(bottomCenter + new Vector3(0,distToGround-0.1f,0),Vector2.right,distToFront+0.1f);
			hit3 =  Physics2D.Raycast(bottomCenter - new Vector3(0,distToGround-0.1f,0),Vector2.right,distToFront+0.1f);
		}else if(Edges.LEFT.Equals(side))
		{
			hit1 =  Physics2D.Raycast(bottomCenter,-Vector2.right,distToFront+0.1f);
			hit2 =  Physics2D.Raycast(bottomCenter + new Vector3(0,distToGround-0.1f,0),-Vector2.right,distToFront+0.1f);
			hit3 =  Physics2D.Raycast(bottomCenter - new Vector3(0,distToGround-0.1f,0),-Vector2.right,distToFront+0.1f);
		}


		if (hit1.collider == null && hit2.collider == null && hit3.collider == null)
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
		PushableScript pushScript = collision.gameObject.GetComponent<PushableScript> ();

		if(pushScript == null || (pushScript != null && !isCollision(Edges.BOTTOM)))
		{
			Edges edge = CollisionHelper.getCollisionEdge (collision);
			Debug.Log ("Plaxer Collision : " + edge.ToString ());
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
		return isCollision (Edges.BOTTOM);
	}


	void Update()
	{

		float inputX = Input.GetAxis("Horizontal");

		if(!letGoLeft)
		{
			letGoLeft = !isCollision(Edges.LEFT);
		}

		if(!letGoRight)
		{
			letGoRight = !isCollision(Edges.RIGHT);
		}



		if ((inputX < 0 && !letGoLeft) || (inputX > 0 && !letGoRight)) {
			inputX = 0;
				}


		movement = speed * inputX;

		if (Input.GetKeyDown ("space") && canJump()){
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
