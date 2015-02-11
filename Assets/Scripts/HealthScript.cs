using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

    /// <summary>
    /// Total hitpoints
    /// </summary>
    public int hp = 1;

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
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Lethal"))
        {
            Damage(hp);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("Lethal"))
        {
            Damage(hp);
        }
    }
}
