using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{
    public Transform shotPrefab;

    public float shootingRateFrom = 0.25f;
    public float shootingRateTo = 0.25f;

    public bool shootForward = true;

    private float shootCooldown;
    private float shootDirX;

    // Use this for initialization
    public void Start()
    {
        shootCooldown = 0f;
        if (shootForward)
        {
            //this.shotPrefab.Rotate(0,0, 90);
            shootDirX = -1.0f;
        }
        else
        {
            //this.shotPrefab.Rotate(0, 0, 90);
            shootDirX = 1.0f;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }

        if(shootForward)
        {
            shootDirX = -1.0f;
        }
        else
        {
            shootDirX = 1.0f;
        }
    }

    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = Random.Range(shootingRateFrom, shootingRateTo);

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab,
                new Vector3(0, 0, 0),
                Quaternion.Euler(new Vector3(0, 0, -90 * shootDirX))) as Transform;

            // Assign position
            shotTransform.position = transform.position;

            // The is enemy property
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Make the weapon shot always towards it
            EnemyMoveScript move = shotTransform.gameObject.GetComponent<EnemyMoveScript>();
            if (move != null)
            {
                move.direction = this.transform.right; // towards in 2D space is the right of the sprite
                move.direction.x *= shootDirX;               
            }
        }
    }

    /// <summary>
    /// Is the weapon ready to create a new projectile?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
