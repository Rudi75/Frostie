using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AssemblyCSharp;
using Assets.Scripts.Utils;

public class EnemyMoveScript : MonoBehaviour
{
    private IEnumerable<Collider2D> Colliders;

    public Vector2 speed = new Vector2(5, 5);
    public Vector2 direction = new Vector2(-1, 0);
    public float jumpHight = 5;
    public bool canJump = false;    
    public float jumpingRateFrom = 0.25f;
    public float jumpingRateTo = 0.5f;
    public bool turnAroundOnCollison = true;
    public bool turnAroundOnPlatformEnd = true;

    private float jumpCooldown;
    private Vector3 currentPos;
    protected Vector2 movement;


    float lastX = 0;
    float lastY = 0;

    public void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        jumpCooldown = 0;
        currentPos = this.transform.position;
        Colliders = transform.GetComponentsInChildren<Collider2D>();
    }

    void Update()
    {
        updateCooldowns();

        checkTurnAroundNow();

        //Jump and/or Movement        
        if(canJumpNow())
        {
            Jump();
        }
        else
        {
            ApplayMovement();
        }        
    }

    protected virtual void ApplayMovement()
    {
        movement = new Vector2(
            speed.x * direction.x,
            speed.y * direction.y);
    }

    protected virtual void Jump()
    {
        jumpCooldown = Random.Range(jumpingRateFrom, jumpingRateTo);
        rigidbody2D.AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {        
        float speedX = movement.x;
        float speedY = rigidbody2D.velocity.y;
        
        rigidbody2D.velocity = new Vector2(speedX, speedY);


        // THIS is fix:
        // if the enemy got stuck at some point, it will continue his movement afterwards
        if (lastX == this.gameObject.transform.position.x && movement.x != 0)
        {
            this.gameObject.transform.Translate(0, -0.2f, 0);
        }
        lastX = this.gameObject.transform.position.x;
        lastY = this.gameObject.transform.position.y;
    }

    private bool canJumpNow()
    {
        var bottomCollider = CollisionHelper.getBottomCollider(Colliders.ToArray());
        if (CollisionHelper.getCollidingObject(bottomCollider, Enums.Edges.BOTTOM, 0.1f) != null 
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
    
    private bool checkTurnAroundNow()
    {

        if(!turnAroundOnPlatformEnd)
        {
            return false;
        }

        Vector3 bottomCenter = gameObject.transform.position;
        
        int layer = 1 << 6;
        layer += 1;
        layer = layer << 2;
        layer = ~layer;

        RaycastHit2D hit = new RaycastHit2D();
        
        hit = Physics2D.Raycast(bottomCenter + new Vector3(1.2f * direction.x, 0, 0), Vector3.down, 10f, layer);

 /*       if (hit.collider == null 
            || !hit.collider.gameObject.name.Contains("Squeezer") 
                && hit.collider.gameObject.tag.Contains("Lethal") 
                && !hit.collider.isTrigger)*/
        if (hit.collider == null || (hit.collider.isTrigger && !hit.collider.name.Contains("Thorn")))
        {
            performTurnAround();
            return true;
        }
/*
        RaycastHit2D[] hits = Physics2D.RaycastAll(bottomCenter + new Vector3(1.2f * direction.x, 0, 0), Vector3.down, 5.5f, layer);

        bool hitOnlyTrigger = true;
        for (int index = 0; index < hits.Length; index++)
        {
            RaycastHit2D actHit = hits[index];
            if (!actHit.collider.isTrigger && !actHit.collider.gameObject.name.Equals("DeathCollider"))
            {
                hitOnlyTrigger = false; 
            }
            else if(actHit.collider.gameObject.name.Equals("DeathCollider"))
            {
                if(hitOnlyTrigger)
                {
                    performTurnAround();
                    return true;
                }
            }
        }*/

        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        var leftCollider = CustomCollisionHelper.getLeftCollider(Colliders.ToArray());
        var rightCollider = CustomCollisionHelper.getRigthCollider(Colliders.ToArray());
        if ((CollisionHelper.getCollidingObject(rightCollider, Enums.Edges.RIGHT, 0.1f) != null
                || CollisionHelper.getCollidingObject(leftCollider, Enums.Edges.LEFT, 0.1f) != null)
            && turnAroundOnCollison)
        {
            performTurnAround();
        }
    }

    protected void performTurnAround()
    {
        direction *= -1;

        gameObject.transform.localScale = new Vector3(
            gameObject.transform.localScale.x * -1,
            gameObject.transform.localScale.y,
            gameObject.transform.localScale.z);

        WeaponScript[] weapons = GetComponentsInChildren<WeaponScript>();

        foreach (WeaponScript weapon in weapons)
        {
            weapon.shootForward = !weapon.shootForward;
        }
    }
}
