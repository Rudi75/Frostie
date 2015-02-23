using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;
using KindsOfDeath = Assets.Scripts.Utils.Enums.KindsOfDeath;
public class PartHealthScript : MonoBehaviour {
    public bool isEnemy = false;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("health: " + other.tag);
        // Is this a shot?
        ShotScript shot = other.gameObject.GetComponent<ShotScript>();
        if ((shot != null) && (shot.isEnemyShot == isEnemy))
        {
            return;
        }
        if (other.tag.Contains("LethalHot"))
        {
            rigidbody2D.gravityScale = 0;
            DieAndStartAnimation(KindsOfDeath.InFire);
            //Die();
        }
        else if (other.tag.Contains("Lethal"))
        {
            rigidbody2D.gravityScale = 0;
            DieAndStartAnimation(KindsOfDeath.Normal);
            //Die();
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        HealthScript healthScript = collision.gameObject.GetComponent<HealthScript>();

        if (collision.collider.tag.Contains("LethalHot") && healthScript == null
        || collision.collider.tag.Contains("LethalHot") && healthScript.isEnemy != isEnemy)
        {
            DieAndStartAnimation(KindsOfDeath.InFire);
            //Die();
        }
        else if (collision.collider.tag.Contains("Lethal") && healthScript == null
       || collision.collider.tag.Contains("Lethal") && healthScript.isEnemy != isEnemy)
        {
            DieAndStartAnimation(KindsOfDeath.Normal);
           // Die();
        }
    }

    public void DieAndStartAnimation(KindsOfDeath deathKind)
    {
        FrostieStatus frostieStatus = transform.parent.GetComponentInChildren<FrostieStatus>();
            if (frostieStatus.isDying)
                return;
            frostieStatus.isDying = true;
            frostieStatus.isFixated = true;

            Animator animator = GetComponentInChildren<Animator>();
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

    public void Die()
    {
        FrostiePartManager frostie = transform.parent.GetComponentInChildren<FrostiePartManager>();
        if (frostie != null)
        {
            frostie.pleaseJustKillYourself();
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
