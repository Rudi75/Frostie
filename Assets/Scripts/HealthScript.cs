﻿using UnityEngine;
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
        FrostieScript frostie = GetComponentInParent<FrostieScript>();
        if (frostie != null)
        {
            frostie.Die(deathKind);
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        FrostieScript frostie = GetComponent<FrostieScript>();
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
        // Is this a shot?
        ShotScript shot = other.gameObject.GetComponent<ShotScript>();
        if ((shot != null) && (shot.isEnemyShot == isEnemy))
        {
            Debug.Log("jop");
            return;
        }
        if (other.tag.Contains("Lethal"))
        {
            DieAndStartAnimation(KindsOfDeath.Normal);
        }
        if (other.tag.Contains("LethalHot"))
        {
            DieAndStartAnimation(KindsOfDeath.InFire);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {        
        HealthScript healthScript = collision.gameObject.GetComponent<HealthScript>();
        if (collision.collider.tag.Contains("Lethal") && healthScript == null
            || collision.collider.tag.Contains("Lethal") && healthScript.isEnemy != isEnemy)
        {
            DieAndStartAnimation(KindsOfDeath.Normal);
        }
        if (collision.collider.tag.Contains("LethalHot") && healthScript == null
        || collision.collider.tag.Contains("LethalHot") && healthScript.isEnemy != isEnemy)
        {
            DieAndStartAnimation(KindsOfDeath.InFire);
        }
    }
}
