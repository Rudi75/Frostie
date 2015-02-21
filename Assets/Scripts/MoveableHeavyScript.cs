using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class MoveableHeavyScript : MoveableScript
{
    private Collider2D bottomCollider;

    void Start()
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        bottomCollider = CollisionHelper.getBotomCollider(colliders);
    }

    override protected void OnCollisionStay2D(Collision2D collision)
    {
        var ground = CollisionHelper.getCollidingObject(bottomCollider, Edges.BOTTOM, 0.1f);
        var freezableGround = ground.GetComponent<FreezableGround>();
        if (freezableGround == null) freezableGround = ground.transform.parent.GetComponent<FreezableGround>();

        if (freezableGround != null && freezableGround.IsFrozen)
        {
            base.OnCollisionStay2D(collision);
        }
    }

    override protected void FixedUpdate()
    {
        var ground = CollisionHelper.getCollidingObject(bottomCollider, Edges.BOTTOM, 0.1f);
        var freezableGround = ground.GetComponent<FreezableGround>();
        if (freezableGround == null) freezableGround = ground.transform.parent.GetComponent<FreezableGround>();

        if (freezableGround != null && freezableGround.IsFrozen)
        {
            base.FixedUpdate();
        }
    }
}