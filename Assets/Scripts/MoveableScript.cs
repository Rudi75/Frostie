using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts.Utils;

public class MoveableScript : MonoBehaviour
{

    private float movement = 0;
    private Vector3 oldPlayerPosition;
    private float speedReduction = 0.5f;

    public GameObject playerLeft;
    public GameObject playerRight;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Death"))
        {
            Destroy(gameObject);
        }
    }

    virtual protected void OnCollisionStay2D(Collision2D collision)
    {
  
        string otherObject = collision.gameObject.tag;
        if (otherObject == "Player")
        {
            FrostieStatus status = collision.gameObject.GetComponent<FrostieStatus>();
            
            if (status != null && !status.isPulling && status.canPushOrPull())
            { 
                Enums.Edges edge = CollisionHelper.getCollisionEdge(collision);

                FrostieMoveScript frostieScript = collision.gameObject.GetComponent<FrostieMoveScript>();
                if (frostieScript != null)
                {
                    float speed = frostieScript.speed;

                    FrostieStatus frostieStatus = collision.gameObject.GetComponent<FrostieStatus>();
                    if (Enums.Edges.RIGHT.Equals(edge) && frostieStatus.isGrounded())
                    {
                        movement = -1 * speed;
                    }

                    if (Enums.Edges.LEFT.Equals(edge) && frostieStatus.isGrounded())
                    {
                        movement = speed;
                    }
                }
            }
        }
    }

    virtual protected void FixedUpdate()
    {
        bool isPull = handlePull();
        if (!isPull)
        {
            float speedX = movement;
            float speedY = rigidbody2D.velocity.y;
            rigidbody2D.velocity = new Vector2(speedX, speedY);
        }
        movement *= speedReduction;


    }

    private bool handlePull()
    {
        

        Vector3 movement = Vector3.zero;
        playerLeft = CollisionHelper.getCollidingObject(collider2D, Enums.Edges.LEFT, 0.5f);
        playerRight = CollisionHelper.getCollidingObject(collider2D, Enums.Edges.RIGHT, 0.5f);

        if ((playerLeft == null || playerLeft.tag != "Player") && (playerRight == null || playerRight.tag != "Player"))
        {
            oldPlayerPosition = Vector3.zero;
            return false;
        }
        GameObject player;
        if (playerRight != null && playerRight.tag == "Player")
            player = playerRight;
        else
            player = playerLeft;

        FrostieStatus status = player.GetComponentInParent<FrostieStatus>();
        if (status != null && status.canPushOrPull() && status.isPulling)
        {
            Vector3 playerPosition = player.transform.position;



            if (!Vector3.zero.Equals(oldPlayerPosition))
            {
                movement.x = playerPosition.x - oldPlayerPosition.x;
            }

            oldPlayerPosition = playerPosition;
            if ((playerRight != null && movement.x > 0)
                || (playerLeft != null && movement.x < 0))
            {
                transform.Translate(movement, Space.World);
                return true;
            }
            else
                return false;
        }
        oldPlayerPosition = Vector3.zero;
        return false;
    }

}