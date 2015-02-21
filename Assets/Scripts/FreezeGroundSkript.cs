﻿using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts.Utils;

public class FreezeGroundSkript : MonoBehaviour 
{
    public KeyCode KeyToFreeze = KeyCode.G;

    private Collider2D bottomCollider;
	// Use this for initialization
	void Start () 
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        bottomCollider = CollisionHelper.getBotomCollider(colliders);
	}
	
    public void freeze()
    {
        var ground = CollisionHelper.getCollidingObject(bottomCollider, Enums.Edges.BOTTOM, 0.1f);
        FreezableGround freezableGround = null;
        if (ground != null) freezableGround = ground.GetComponent<FreezableGround>();
        if (ground != null && freezableGround == null) freezableGround = ground.transform.parent.GetComponent<FreezableGround>();

        if (freezableGround != null)
        {
            freezableGround.Freeze();
        }

        var groundCollider = CustomCollisionHelper.getBiggestCollider(ground.GetComponentsInChildren<Collider2D>());
        var left = CollisionHelper.getCollidingObject(groundCollider, Enums.Edges.LEFT, 0.3f);
        if (left != null) freezableGround = left.GetComponent<FreezableGround>();
        if (left != null && freezableGround == null) freezableGround = left.transform.parent.GetComponent<FreezableGround>();

        if (freezableGround != null)
        {
            freezableGround.Freeze();
        }

        var rigth = CollisionHelper.getCollidingObject(groundCollider, Enums.Edges.RIGHT, 0.3f);
        if (rigth != null) freezableGround = rigth.GetComponent<FreezableGround>();
        if (rigth != null && freezableGround == null) freezableGround = rigth.transform.parent.GetComponent<FreezableGround>();

        if (freezableGround != null)
        {
            freezableGround.Freeze();
        }
    }
	
}
