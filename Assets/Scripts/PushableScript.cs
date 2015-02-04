﻿using UnityEngine;
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
	}

	void OnCollisionStay2D(Collision2D collision)
	{

		string otherObject = collision.gameObject.tag;
		if (otherObject == "Player") 
		{
			Edges edge = CollisionHelper.getCollisionEdge (collision);

			FrostieScript frostieScript = collision.gameObject.GetComponent<FrostieScript>();

			float speed = frostieScript.speed;
			
			if (Edges.RIGHT.Equals(edge) && frostieScript.isGrounded()) {
				movement= -1 * speed;
			}
			
			if (Edges.LEFT.Equals(edge) && frostieScript.isGrounded()){
				movement= speed;
			}
		}
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