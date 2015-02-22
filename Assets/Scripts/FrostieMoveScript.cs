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

    private FrostiePartManager partManager;
    private FrostieStatus frostieStatus;
    


    private float movement;
    


    public void Start()
    {
       partManager = GetComponentInChildren<FrostiePartManager>();
       frostieStatus = GetComponentInChildren<FrostieStatus>();
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        MoveableScript pushScript = collision.gameObject.GetComponent<MoveableScript>();

        if (!frostieStatus.IsMelted && (pushScript == null || (pushScript != null && !frostieStatus.isGrounded())))
        {
            Debug.Log("----" + collision.gameObject.name);
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
        viewDirection = partManager.getActivePart().transform.localScale.x / Mathf.Abs(partManager.getActivePart().transform.localScale.x);
        float inputX = frostieStatus.isFixated ? 0 : Input.GetAxis("Horizontal");

        if (viewDirection * inputX < 0 && !frostieStatus.isPulling)
        {
            Vector3 scale = partManager.getActivePart().transform.localScale;
            scale.x *= -1;
            partManager.getActivePart().transform.localScale = scale;
            viewDirection = inputX;
        }


        Collider2D[] colliders = partManager.getActivePart().GetComponentsInChildren<Collider2D>();
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



        frostieStatus.IsWalking = (movement != 0.0f);

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
        Debug.Log("Jump " + partManager.getActivePart().name);
        frostieStatus.isFixated = false;
        partManager.getActivePart().rigidbody2D.AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
        
    }

    void FixedUpdate()
    {
        float speedX = movement;
        float speedY = partManager.getActivePart().rigidbody2D.velocity.y;
        partManager.getActivePart().rigidbody2D.velocity = new Vector2(speedX, speedY);
    }


}
