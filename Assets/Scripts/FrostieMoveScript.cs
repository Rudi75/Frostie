using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;
using KindsOfDeath = Assets.Scripts.Utils.Enums.KindsOfDeath;
using AssemblyCSharp;

public class FrostieMoveScript : MonoBehaviour {
    public float speed = 10;
    public float jumpHight = 40;
    public bool letGoRight = true;
    public bool letGoLeft = true;
    public float viewDirection = +1;
    public float inputXAxis;

    private FrostiePartManager partManager;
    private FrostieStatus frostieStatus;

    private bool isWalking = false;


 
    


    private float movement;
    


    public void Start()
    {
        partManager = transform.parent.GetComponentInChildren<FrostiePartManager>();
        frostieStatus = transform.parent.GetComponentInChildren<FrostieStatus>();
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        MoveableScript pushScript = collision.gameObject.GetComponent<MoveableScript>();

        if (frostieStatus != null && !frostieStatus.IsMelted && (pushScript == null || (pushScript != null && !frostieStatus.isGrounded())))
        {
            Enums.Edges edge = CollisionHelper.getCollisionEdge(collision);
            if (Enums.Edges.RIGHT.Equals(edge))
            {
                letGoRight = false;
            }

            if (Enums.Edges.LEFT.Equals(edge))
            {
                letGoLeft = false;
            }
        }
    }

    void Update()
    {
        viewDirection = transform.localScale.x / Mathf.Abs(transform.localScale.x);
        float inputX = frostieStatus.isFixated ? 0 : inputXAxis;

        if (viewDirection * inputX < 0 && !frostieStatus.isPulling)
        {
            Vector3 scale = partManager.getActivePart().transform.localScale;
            scale.x *= -1;
            partManager.getActivePart().transform.localScale = scale;
            viewDirection = inputX;
        }


        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        Collider2D bottomCollider = CollisionHelper.getBottomCollider(colliders);



        if (!letGoLeft)
        {
            letGoLeft = CollisionHelper.getCollidingObject(bottomCollider, Enums.Edges.LEFT, 0.1f) == null;
        }

        if (!letGoRight)
        {
            letGoRight = CollisionHelper.getCollidingObject(bottomCollider, Enums.Edges.RIGHT, 0.1f) == null;
        }

        if ((inputX < 0 && !letGoLeft) || (inputX > 0 && !letGoRight) || (viewDirection * inputX > 0 && frostieStatus.isPulling))
        {
            inputX = 0;
        }

        movement = speed * inputX;



       bool value = (movement != 0.0f);

        if (isWalking != value)
        {
            isWalking = value;

            FrostieAnimationManager frostieAnimationManager = GetComponent<FrostieAnimationManager>();
            if (frostieAnimationManager != null)
            {
                frostieAnimationManager.animateWalking(isWalking);
            }
            else
            {
                Animator animator = GetComponentInChildren<Animator>();
                animator.SetBool("IsWalking", isWalking);
            }
        }

        
    }

    public void Jump()
    {
        frostieStatus.isFixated = false;
        rigidbody2D.AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
        
    }

    void FixedUpdate()
    {
        float speedX =  movement;
        float speedY = rigidbody2D.velocity.y;
        rigidbody2D.velocity = new Vector2(speedX, speedY);
    }


}
