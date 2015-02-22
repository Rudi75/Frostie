using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using Assets.Scripts.Utils;
using System.Linq;

public class RabbitMoveScript : EnemyMoveScript
{
    private Animator animator;

    protected override void Init()
    {
        base.Init();
        animator = GetComponentInChildren<Animator>();
    }

    protected override void ApplayMovement()
    {
        base.ApplayMovement();
        if (animator != null)
        {
            bool isMoving = (movement.x != 0.0f);
            if (animator.GetBool("IsWalking") != isMoving)
                animator.SetBool("IsWalking", isMoving);
        }
    }

    protected override void Jump()
    {
        if (animator != null) animator.SetTrigger("Jump");
        base.Jump();
    }
}
