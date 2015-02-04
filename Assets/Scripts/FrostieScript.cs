using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class FrostieScript : MonoBehaviour {
	
	public float speed = 10;
	public float jumpHight = 5;
	public bool letGoRight = true;
	public bool letGoLeft = true;
	

	private float movement;

    private Vector3 oltPlatformPosition = Vector3.zero;

	void OnCollisionEnter2D(Collision2D collision)
	{
		PushableScript pushScript = collision.gameObject.GetComponent<PushableScript> ();

        if (pushScript == null || (pushScript != null && CollisionHelper.getCollidingObject(getBottom(), Edges.BOTTOM) == null))
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

	void Update()
	{

		float inputX = Input.GetAxis("Horizontal");

		if(!letGoLeft)
		{
			letGoLeft = CollisionHelper.getCollidingObject(getBottom(),Edges.LEFT) == null;
		}

		if(!letGoRight)
		{
            letGoRight = CollisionHelper.getCollidingObject(getBottom(), Edges.RIGHT) == null;
		}



		if ((inputX < 0 && !letGoLeft) || (inputX > 0 && !letGoRight)) {
			inputX = 0;
				}


		movement = speed * inputX;

        handleMovingPlatforms();

		if (Input.GetKeyDown("space") && isGrounded()){
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

    public bool isGrounded()
    {
        return CollisionHelper.getCollidingObject(getBottom(), Edges.BOTTOM) != null;
    }


    private Collider2D getBottom()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "FrostieBottom")
            {
                return child.collider2D;
            }
        }
        return null;
    }

    private void handleMovingPlatforms()
    {
        Vector3 movement = Vector3.zero;
        GameObject platform = CollisionHelper.getCollidingObject(getBottom(), Edges.BOTTOM);

        if (platform == null || platform.tag != "Platform")
        {
            oltPlatformPosition = Vector3.zero;
            return;
        }
        Vector3 platformPosition = platform.transform.position;
        if (!Vector3.zero.Equals(oltPlatformPosition))
        {
            movement = platformPosition - oltPlatformPosition;
        }
        oltPlatformPosition = platformPosition;

        transform.Translate(movement, Space.World);
    }

}
