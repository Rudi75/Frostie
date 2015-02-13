using UnityEngine;
using System.Collections;

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
            FollowChild parent = GetComponentInParent<FollowChild>();
            if(parent != null)
            {
                Destroy(parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        Damage(hp);
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
            Die();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("Lethal"))
        {
            Die();
        }
    }
}
