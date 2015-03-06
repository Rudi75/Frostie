using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts.Utils;
using System.Collections.Generic;

public class FreezeGroundSkript : MonoBehaviour 
{
    private FrostieSoundManager soundManager;

    private Collider2D bottomCollider;

	// Use this for initialization
	void Start () 
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        bottomCollider = CollisionHelper.getBottomCollider(colliders);
        soundManager = GetComponentInParent<FrostieSoundManager>();
	}
	

	public void FreezeGround() 
    {
        GameObject ground = null;
        List<GameObject> hits = CollisionHelper.getCollidingObject(bottomCollider, Enums.Edges.BOTTOM, 0.1f);
        foreach (GameObject hit in hits)
        {
            if (hit != null)
                ground = hit;
        }

        FreezableGround freezableGround = null;
        if (ground != null) freezableGround = ground.GetComponent<FreezableGround>();
        if (ground != null && freezableGround == null) freezableGround = ground.transform.parent.GetComponent<FreezableGround>();
        freezeGround(freezableGround);

        var groundCollider = CustomCollisionHelper.getBiggestCollider(ground.GetComponentsInChildren<Collider2D>());
        GameObject left = null;
        hits = CollisionHelper.getCollidingObject(groundCollider, Enums.Edges.LEFT, 0.3f);
        foreach (GameObject hit in hits)
        {
            if (hit != null)
                left = hit;
        }

        if (left != null) freezableGround = left.GetComponent<FreezableGround>();
        if (left != null && freezableGround == null) freezableGround = left.transform.parent.GetComponent<FreezableGround>();
        freezeGround(freezableGround);

        GameObject rigth = null;
        hits = CollisionHelper.getCollidingObject(groundCollider, Enums.Edges.RIGHT, 0.3f);
        foreach (GameObject hit in hits)
        {
            if (hit != null)
                rigth = hit;
        }

        if (rigth != null) freezableGround = rigth.GetComponent<FreezableGround>();
        if (rigth != null && freezableGround == null) freezableGround = rigth.transform.parent.GetComponent<FreezableGround>();
        freezeGround(freezableGround);
    }

    private void freezeGround(FreezableGround freezableGround)
    {
        if (freezableGround != null)
        {
            freezableGround.Freeze();
            if (soundManager != null)
            {
                soundManager.playFreezingGroundSound();
            }
        }
    }
	
}
