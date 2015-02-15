using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

using KindsOfDeath = Assets.Scripts.Utils.Enums.KindsOfDeath;

public class HeatDamageOnCollide : MonoBehaviour
{
    public int Damage = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        FrostieScript frostie = collision.gameObject.GetComponentInParent<FrostieScript>();
        if (frostie != null)
        {
            frostie.Die(KindsOfDeath.InFire);
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
