using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;
using KindsOfDeath = Assets.Scripts.Utils.Enums.KindsOfDeath;
using AssemblyCSharp;

public class FrostieAnimationManager : MonoBehaviour {

    private Animator animator;
    private FrostieStatus frostieStatus;

    

    public void Start()
    {
        animator = GetComponentInParent<Animator>();
        frostieStatus = GetComponent<FrostieStatus>();
        frostieStatus.IsMelted = false;
    }

    
    public void animateWalking(bool isWalking)
    {
        animator.SetBool("IsWalking", isWalking);
    }

    public void animateMelting(bool isMelted)
    {
        animator.SetBool("isMelted", isMelted);
    }

   public void animateDeath(KindsOfDeath deathKind)
 {
     switch (deathKind)
     {
         case KindsOfDeath.Normal:
             animator.SetTrigger("Death");
             break;
         case KindsOfDeath.InFire:
             animator.SetTrigger("DeathInFire");
             break;
         case KindsOfDeath.Squeezed:
             break;
     }
 }

    public void animateJump()
    {
        animator.SetTrigger("Jump");
    }

}
