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
    public bool isPartMising { get; set; }
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
        return CollisionHelper.getCollidingObject(frostiePartManager.getBasePart().collider2D, Enums.Edges.BOTTOM, 0.1f) != null;
    }

    public bool canJump()
    {
        if (isMelted)
            return false;
        else
            return isGrounded();
    }
}
