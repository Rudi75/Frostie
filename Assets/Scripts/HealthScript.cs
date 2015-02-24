using UnityEngine;
using System.Collections;

using Assets.Scripts.Utils;
using KindsOfDeath = Assets.Scripts.Utils.Enums.KindsOfDeath;

public class HealthScript : MonoBehaviour {

    /// <summary>
    /// Total hitpoints
    /// </summary>
    public int hp = 1;
    public bool isEnemy = true;

    /// <summary>
    /// Inflicts damage and check if the object should be destroyed
    /// </summary>
    /// <param name="damageCount"></param>
    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            // Dead!
            DieAndStartAnimation(KindsOfDeath.Normal);
        }
    }


    public void DieAndStartAnimation(KindsOfDeath deathKind)
    {
        FrostieStatus frostieStatus = GetComponent<FrostieStatus>();
        if (frostieStatus != null)
        {
            if (frostieStatus.isDying)
                return;
            frostieStatus.isDying = true;
            frostieStatus.isFixated = true;

            FrostieAnimationManager frostieAnimationManager = GetComponent<FrostieAnimationManager>();
            frostieAnimationManager.animateDeath(deathKind);
        }
        else
        {
            var deathAnim = GetComponent<DeathAnimation>();
            if (deathAnim != null)
            {
                deathAnim.Kill();
            }
            else
            {
                Die();
            }
        }
    }

    public void Die()
    {
        FrostiePartManager frostie = GetComponent<FrostiePartManager>();
        if (frostie != null)
        {
            frostie.pleaseJustKillYourself();
        }
        else
        {
            Destroy(gameObject);
        }
       
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        ShotScript shot = other.gameObject.GetComponent<ShotScript>();
        if ((shot != null) && (shot.isEnemyShot == isEnemy))
        {
            return;
        }
        if (other.tag.Contains("LethalHot"))
        {
            DieAndStartAnimation(KindsOfDeath.InFire);
        }else if (other.tag.Contains("Lethal"))
        {
            DieAndStartAnimation(KindsOfDeath.Normal);
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {        
        HealthScript healthScript = collision.gameObject.GetComponent<HealthScript>();
        if (collision.collider.tag.Contains("LethalHot") && healthScript == null
        || collision.collider.tag.Contains("LethalHot") && healthScript.isEnemy != isEnemy)
        {
            DieAndStartAnimation(KindsOfDeath.InFire);
        }else if ((collision.collider.tag.Contains("Lethal") && healthScript == null)
        || (collision.collider.tag.Contains("Lethal") && healthScript.isEnemy != isEnemy))
        {
            DieAndStartAnimation(KindsOfDeath.Normal);
        }
    }
}
