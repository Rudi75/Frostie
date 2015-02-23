using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemyScript : MonoBehaviour
{
    private bool hasSpawn;
    private EnemyMoveScript moveScript;
    private WeaponScript[] weapons;

    private IEnumerable<SpriteRenderer> renderers;

    void Awake()
    {
        // Retrieve the weapon only once
        weapons = GetComponentsInChildren<WeaponScript>();

        // Retrieve scripts to disable when not spawn
        moveScript = GetComponent<EnemyMoveScript>();

        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // 1 - Disable everything
    void Start()
    {
        hasSpawn = false;

        // Disable everything
        // -- Moving
        moveScript.enabled = false;
        // -- Shooting
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }
    }

    void Update()
    {
        // 2 - Check if the enemy has spawned.
        if (hasSpawn == false)
        {
            if (IsVisibleFromCamera())
            {
                Spawn();
            }
        }
        else
        {
            // Auto-fire
            foreach (WeaponScript weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    weapon.Attack(true);
                }
            }

            // 4 - Out of the camera ? Destroy the game object.
            if (!IsVisibleFromCamera())
            {
             //   Destroy(gameObject);
            }
        }
    }

    private bool IsVisibleFromCamera()
    {
        var query = from obj in renderers where obj.IsVisibleFrom(Camera.main) select obj;
        if (query.Any())
        {
            return true;
        }
        return false;
    }

    // 3 - Activate itself.
    private void Spawn()
    {
        hasSpawn = true;

        // Enable everything
        // -- Moving
        moveScript.enabled = true;
        // -- Shooting
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }
    }
}