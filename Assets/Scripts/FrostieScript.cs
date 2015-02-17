using UnityEngine;
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
               // Debug.Log("i running");
                animator.SetBool("IsWalking", isWalking);
            }
        }
    }

    private Transform head;
    private Transform middlePart;
    private Transform basePart;
    private Transform frotieParent;
    private Vector3 headLocalPosition;
    private Vector3 middleLocalPosition;
    private Vector3 baseLocalPosition;
    private Vector3 distToBase;
    public bool isPartMising;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        isMelted = false;
        foreach (Transform childTransform in transform)
        {
            foreach (Transform child in childTransform)
            {
                if (child.name.Contains("Head"))
                    head = child;
                else if (child.name.Contains("Middle"))
                    middlePart = child;
                else if (child.name.Contains("Base"))
                    basePart = child;
            }
                       
        }
        middleLocalPosition = middlePart.localPosition;
        baseLocalPosition = basePart.localPosition;
        headLocalPosition = head.localPosition;
        frotieParent = head.parent;
        distToBase = basePart.position - transform.position;
        isPartMising = false;
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
		MoveableScript pushScript = collision.gameObject.GetComponent<MoveableScript> ();

        if (!isMelted && (pushScript == null || (pushScript != null && CollisionHelper.getCollidingObject(basePart.collider2D, Edges.BOTTOM, 0.1f) == null)))
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


        if(Input.GetKeyDown(KeyCode.LeftAlt) && !isPartMising)
        {
            ChangeMeltedState();
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            recallParts();
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
            letGoLeft = CollisionHelper.getCollidingObject(basePart.collider2D, Edges.LEFT, 0.1f) == null;
		}

		if(!letGoRight)
		{
            letGoRight = CollisionHelper.getCollidingObject(basePart.collider2D, Edges.RIGHT, 0.1f) == null;
		}



		if ((inputX < 0 && !letGoLeft) || (inputX > 0 && !letGoRight) || (viewDirection*inputX > 0 && Input.GetKey(KeyCode.LeftControl))) {
			inputX = 0;
				}

		movement = speed * inputX;
        IsWalking = (movement != 0.0f);

        if (Input.GetKeyDown(KeyCode.Space) && canJump())
        {
			//rigidbody2D.AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
            isFixated = true;
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

    public void Jump()
    {
        isFixated = false;
        rigidbody2D.AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
    }
	
	void FixedUpdate()
	{
		float speedX = movement;
		float speedY = rigidbody2D.velocity.y;
		rigidbody2D.velocity = new Vector2(speedX,speedY);
	}

    public bool isGrounded()
    {
        return CollisionHelper.getCollidingObject(basePart.collider2D, Edges.BOTTOM, 0.1f) != null;
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
    void recallParts()
    {


        getRidOfDummies(head);
        head.localScale = new Vector3(1, 1, 1);

        getRidOfDummies(basePart);
        basePart.localScale = new Vector3(1, 1, 1);

        getRidOfDummies(middlePart);
        middlePart.localScale = new Vector3(1, 1, 1);


        ThrowHeadScript headAim = head.GetComponentInChildren<ThrowHeadScript>();
        if (headAim != null)
        {
            headAim.reset();
        }

        transform.position = basePart.position -  distToBase;

        head.localPosition = headLocalPosition;
        middlePart.localPosition = middleLocalPosition;
        basePart.localPosition = baseLocalPosition;
        isPartMising = false;
    }

    public void pleaseJustKillYourself()
    {
        Destroy(head.gameObject);
        Destroy(middlePart.gameObject);
        Destroy(basePart.gameObject);
        Destroy(transform.parent.gameObject);
    }

    private void getRidOfDummies(Transform part)
    {
        if (part.parent == null)
            return;

        getRidOfDummies(part.parent);

        
        if( part.parent.name.Contains("Dummy"))
        {
            Transform dummy = part.parent; 
            part.parent = frotieParent;
            Destroy(dummy.gameObject);           
        }
    }
}
