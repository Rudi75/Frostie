using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;
using KindsOfDeath = Assets.Scripts.Utils.Enums.KindsOfDeath;
using Faces = Assets.Scripts.Utils.Enums.Faces;
using Mood = Assets.Scripts.Utils.Enums.Mood;
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
    public bool isDying { get; set; }

    private Faces face;
    public Faces IsFaceing
    {
        get { return face; }
        set 
        {
            if (face != value && !IsMelted && !isDying && !isPulling)
            {
                face = value;
                frostieAnimationManager.animateFacing(face);
            }
        } 
    }

    private Mood mood;
    public Mood IsInMood
    {
        get { return mood; }
        set
        {
            if (mood != value)
            {
                mood = value;
                var heads = GetComponentsInChildren<MoodScript>(true);
                foreach (var head in heads)
                {
                    head.SetMood(mood);
                }
            }
        }
    }

    public void Start()
    {
        frostieAnimationManager = GetComponent<FrostieAnimationManager>();
        frostiePartManager = GetComponent<FrostiePartManager>();
        isFixated = false;
        face = Faces.SIDE;
        mood = Mood.SAD;
    }


    public bool isGrounded()
    {
  
        Collider2D[] colliders = frostiePartManager.getActivePart().GetComponentsInChildren<Collider2D>();
        Collider2D bottomCollider = CollisionHelper.getBottomCollider(colliders);
        return CollisionHelper.isCollision(bottomCollider, Enums.Edges.BOTTOM, 0.1f);
    }

    public bool canJump()
    {
        if (isMelted || isFixated || frostiePartManager.getActivePart().Equals(frostiePartManager.getHeadClone()))
            return false;
        else
            return isGrounded();
    }

    

    public bool canMelt()
    {
        GameObject headClone = frostiePartManager.getHeadClone();
        GameObject middleClone = frostiePartManager.getMiddlePartClone();
        GameObject headAndMiddleClone = frostiePartManager.getHeadAndMiddleClone();
        return headClone == null && middleClone == null && headAndMiddleClone == null && !isFixated;
    }

    public bool canPushOrPull()
    {
        //GameObject headClone = frostiePartManager.getHeadClone();
        GameObject middleClone = frostiePartManager.getMiddlePartClone();
        GameObject headAndMiddleClone = frostiePartManager.getHeadAndMiddleClone();
        return /*headClone == null &&*/ middleClone == null && headAndMiddleClone == null && !isMelted;
    }

    public bool canSpeak()
    {
        GameObject headClone = frostiePartManager.getHeadClone();
        GameObject headAndMiddleClone = frostiePartManager.getHeadAndMiddleClone();
        return headClone == null && headAndMiddleClone == null;

    }

    internal bool canTurnWheel()
    {
        GameObject middleClone = frostiePartManager.getMiddlePartClone();
        GameObject headAndMiddleClone = frostiePartManager.getHeadAndMiddleClone();
        return middleClone == null && headAndMiddleClone == null && !isMelted;
    }
}
