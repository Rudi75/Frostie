using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class EnemyMoveScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(5, 5);
    public Vector2 direction = new Vector2(-1, 0);
    public float jumpHight = 5;
    public bool canJump = false;    
    public float jumpingRate = 0.25f;
    
    private float jumpCooldown;
    private Vector3 currentPos;
    private Vector2 movement;

    void Start()
    {
        jumpCooldown = 0;
        currentPos = this.transform.position;        
    }

    void Update()
    {
        updateCooldowns();
     
        //Jump and/or Movement        
        if(canJumpNow())
        {
            jumpCooldown = jumpingRate;
            rigidbody2D.AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
        }
        else
        {
            movement = new Vector2(
                speed.x * direction.x,
                speed.y * direction.y);
        }        
    }

    void FixedUpdate()
    {        
        float speedX = movement.x;
        float speedY = rigidbody2D.velocity.y;
        rigidbody2D.velocity = new Vector2(speedX, speedY);
    }

    private bool canJumpNow()
    {
        if (CollisionHelper.getCollidingObject(this.transform.collider2D, Edges.BOTTOM, 0.1f) != null 
            && jumpCooldown <= 0
            && canJump == true)
        {
            return true;
        }
        return false;
    }

    void updateCooldowns()
    {
        if (jumpCooldown > 0)
        {
            jumpCooldown -= Time.deltaTime;
        }

    }
}
