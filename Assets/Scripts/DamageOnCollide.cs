using UnityEngine;
using System.Collections;

public class DamageOnCollide : MonoBehaviour 
{
    public int Damage = 1;

    void OnTriggerEnter2D(Collider2D collider)
    {
        HealthScript livingBeeing = collider.gameObject.GetComponentInParent<HealthScript>();
        if (livingBeeing != null)
        {
            livingBeeing.Damage(Damage);
            return;
        }
    }
}
