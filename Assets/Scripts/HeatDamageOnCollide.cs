using UnityEngine;
using System.Collections;

public class HeatDamageOnCollide : MonoBehaviour
{
    public int Damage = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        FrostieScript frostie = collision.gameObject.GetComponent<FrostieScript>();
        if (frostie != null)
        {
            frostie.melt();
            return;
        }

        HealthScript livingBeeing = collision.gameObject.GetComponent<HealthScript>();
        if (livingBeeing != null)
        {
            livingBeeing.Damage(Damage);
            return;
        }
    }
}
