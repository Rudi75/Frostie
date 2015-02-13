using UnityEngine;
using System.Collections;

public class EnemyMoveScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(5, 5);
    public Vector2 direction = new Vector2(-1, 0);

    private Vector2 movement;

    void Start()
    {

    }

    void Update()
    {
        // 2 - Movement
        movement = new Vector2(
            speed.x * direction.x,
            speed.y * direction.y);
    }

    void FixedUpdate()
    {        
        rigidbody2D.velocity = movement;
    }
}
