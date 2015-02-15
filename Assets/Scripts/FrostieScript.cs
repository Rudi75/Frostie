﻿using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts.Utils;

using KindsOfDeath = Assets.Scripts.Utils.Enums.KindsOfDeath;

public class FrostieScript : MonoBehaviour {
	
	public float speed = 10;
	public float jumpHight = 5;
	public bool letGoRight = true;
	public bool letGoLeft = true;
    public float viewDirection = +1;

    public bool isMelted { get; set; }
	private float movement;

    private Animator animator;

    private bool isFixated = false;

    private bool isWalking;
    public bool IsWalking 
    {
        set
        {
            if (isWalking != value)
            {
                isWalking = value;
                Debug.Log("i running");
                animator.SetBool("IsWalking", isWalking);
            }
        }
    }

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        isMelted = false;
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
		MoveableScript pushScript = collision.gameObject.GetComponent<MoveableScript> ();

        if (!isMelted && (pushScript == null || (pushScript != null && CollisionHelper.getCollidingObject(getBottomCollider(), Edges.BOTTOM,0.1f) == null)))
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


        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ChangeMeltedState();
        }


		float inputX = isFixated ? 0 : Input.GetAxis("Horizontal");
        if (viewDirection * inputX < 0 && !Input.GetKey(KeyCode.LeftControl))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            viewDirection = inputX;
        }

		if(!letGoLeft)
		{
            letGoLeft = CollisionHelper.getCollidingObject(getBottomCollider(), Edges.LEFT, 0.1f) == null;
		}

		if(!letGoRight)
		{
            letGoRight = CollisionHelper.getCollidingObject(getBottomCollider(), Edges.RIGHT, 0.1f) == null;
		}



		if ((inputX < 0 && !letGoLeft) || (inputX > 0 && !letGoRight) || (viewDirection*inputX > 0 && Input.GetKey(KeyCode.LeftControl))) {
			inputX = 0;
				}

		movement = speed * inputX;
        IsWalking = (movement != 0.0f);

        if (Input.GetKeyDown(KeyCode.Space) && canJump())
        {
			//rigidbody2D.AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
		} 
		
		// Make sure we are not outside the camera bounds
		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;

        var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
			).y;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, transform.position.y, topBorder),//bottomBorder
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
        return CollisionHelper.getCollidingObject(getBottomCollider(), Edges.BOTTOM, 0.1f) != null;
    }

    public void ChangeMeltedState()
    {
        isMelted = !isMelted;
        animator.SetBool("isMelted", isMelted);
        movement = 0;
        Debug.Log("isMelted = " + isMelted);
    }

    private bool canJump()
    {
        if (isMelted)
            return false;
        else
            return isGrounded();
    }


    private Collider2D getBottomCollider()
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

    private bool isDying;
    public void Die(KindsOfDeath deathKind)
    {
        if (isDying)
            return;

        isDying = true;
        isFixated = true;
        switch (deathKind)
        {
            case KindsOfDeath.Normal:
                animator.SetTrigger("Death");
                break;
            case KindsOfDeath.InFire:
                animator.SetTrigger("DeathInFire");
                break;
            case KindsOfDeath.Squeezed:
                break;
        }   
    }

}
