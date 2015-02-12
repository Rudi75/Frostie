using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class GetSqeezedScript : MonoBehaviour {

    private bool squeezed = false;
    private SqeezerAnimationScript sqeeezerScript;
    private Vector3 startScale;
    private Collider2D[] colliders;
    private float mySize;

    public void Start()
    {
        startScale = transform.localScale;
        colliders = GetComponentsInChildren<Collider2D>();
    }

    public void FixedUpdate()
    {
        if (!squeezed)
        {
            Collider2D topCollider = CollisionHelper.getTopCollider(colliders);
            Collider2D bottomCollider = CollisionHelper.getBotomCollider(colliders);

            GameObject ground = CollisionHelper.getCollidingObject(bottomCollider, Edges.BOTTOM, 0.1f);
            GameObject squeezer = CollisionHelper.getCollidingObject(topCollider, Edges.TOP, 0.1f);

            if (ground != null && squeezer != null)
            {
                sqeeezerScript = squeezer.GetComponentInParent<SqeezerAnimationScript>();
                if (sqeeezerScript != null && sqeeezerScript.direction == -1)
                {
                    mySize = squeezer.transform.position.y - ground.transform.position.y;
                    squeezed = true;
                }
            }
        }
        else
        {

            //squeezed = false;
            float speed = sqeeezerScript.distanceToMovePerFrame * Time.deltaTime;
            float percentageOfSize = speed/mySize;
            Vector3 myScale = transform.localScale;
            myScale.y = myScale.y - (myScale.y * percentageOfSize * 2);
            transform.localScale = myScale;
            if (myScale.y <= 0.4 * startScale.y)
            {
                HealthScript healthScript = GetComponent<HealthScript>();
                if(healthScript != null)
                {
                    healthScript.Die();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
                
        }
    }
}
