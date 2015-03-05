using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts.Utils;
using System.Collections.Generic;

public class MoveableHeavyScript : MoveableScript
{
    private Collider2D bottomCollider;

    void Start()
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        bottomCollider = CollisionHelper.getBottomCollider(colliders);
    }

    override protected void OnCollisionStay2D(Collision2D collision)
    {
        FreezableGround freezableGround = getFreezableGround();

        if (freezableGround != null && freezableGround.IsFrozen)
        {
            base.OnCollisionStay2D(collision);
        }
    }

    override protected void FixedUpdate()
    {
        FreezableGround freezableGround = getFreezableGround();
        if (freezableGround != null && freezableGround.IsFrozen)
        {
            base.FixedUpdate();
        }
    }

    private FreezableGround getFreezableGround()
    {
        FreezableGround freezableGround = null;
        try
        {
            List<GameObject> hits = CollisionHelper.getCollidingObject(bottomCollider, Enums.Edges.BOTTOM, 0.1f);
            foreach (GameObject hit in hits)
            {
                if (hit != null)
                {
                    var temp = hit.GetComponent<FreezableGround>();
                    if (temp != null)
                        freezableGround = temp;
                }
            }

            if (freezableGround == null)
            {
                foreach (GameObject hit in hits)
                {
                    if (hit != null)
                    {
                        var temp = hit.transform.parent.GetComponent<FreezableGround>();
                        if (temp != null)
                            freezableGround = temp;
                    }
                }
            }
        }
        catch(System.NullReferenceException)
        {
            freezableGround = null;
        }
        return freezableGround;
    }
}