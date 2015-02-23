using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;
using KindsOfDeath = Assets.Scripts.Utils.Enums.KindsOfDeath;
using AssemblyCSharp;

public class FrostieStatus : MonoBehaviour {

    private FrostieAnimationManager frostieAnimationManager;
    private FrostiePartManager frostiePartManager;

    public bool isPulling { get; set; }

    public bool isFixated { get; set; }
    private bool isMelted;
    public bool IsMelted
    {
        set
        {
            if (isMelted != value)
            {
                isMelted = value;
                // Debug.Log("i running");
                frostieAnimationManager.animateMelting(isMelted);
            }
        }
        get
        {
            return isMelted;
        }
    }

    private bool isWalking;
    public bool IsWalking
    {
        set
        {
            if (isWalking != value)
            {
                isWalking = value;
                // Debug.Log("i running");
                frostieAnimationManager.animateWalking(isWalking);
            }
        }
    }
    public bool isDying { get; set; }

    public void Start()
    {
        frostieAnimationManager = GetComponent<FrostieAnimationManager>();
        frostiePartManager = GetComponent<FrostiePartManager>();
        isFixated = false;
    }


    public bool isGrounded()
    {
  
        Collider2D[] colliders = frostiePartManager.getActivePart().GetComponentsInChildren<Collider2D>();
        Collider2D bottomCollider = CollisionHelper.getBottomCollider(colliders);
        return CollisionHelper.getCollidingObject(bottomCollider, Enums.Edges.BOTTOM, 0.1f) != null;
    }

    public bool canJump()
    {
        if (isMelted)
            return false;
        else
            return isGrounded();
    }

    

    public bool canMelt()
    {
        GameObject headClone = frostiePartManager.getHeadClone();
        GameObject middleClone = frostiePartManager.getMiddlePartClone();
        GameObject headAndMiddleClone = frostiePartManager.getHeadAndMiddleClone();
        return headClone == null && middleClone == null && headAndMiddleClone == null;
    }

    public bool canPushOrPull()
    {
        GameObject headClone = frostiePartManager.getHeadClone();
        GameObject middleClone = frostiePartManager.getMiddlePartClone();
        GameObject headAndMiddleClone = frostiePartManager.getHeadAndMiddleClone();
        return headClone == null && middleClone == null && headAndMiddleClone == null;
    }
}
