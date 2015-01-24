using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {
	
	private Vector2 movement;
	
	void FixedUpdate()
	{
		rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
	}
}