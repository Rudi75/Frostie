using UnityEngine;
using System.Collections;

public class HeatDamageOnCollide : MonoBehaviour
{
    public int Damage = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        FrostieScript frostie = collision.gameObject.GetComponentInParent<FrostieScript>();
        if (frostie != null && !frostie.isMelted)
        {
            frostie.ChangeMeltedState();
            return;
        }

        HealthScript livingBeeing = collision.gameObject.GetComponentInParent<HealthScript>();
        if (livingBeeing != null)
        {
            livingBeeing.Damage(Damage);
            return;
        }
    }
}
