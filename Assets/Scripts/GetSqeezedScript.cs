using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts.Utils;
using System.Collections.Generic;

public class GetSqeezedScript : MonoBehaviour {

    public bool squeezed = false;
    public SqeezerAnimationScript sqeeezerScript;
    private Vector3 startScale;
    
    private float mySize;

    private bool isYDirection;
    public void Start()
    {
        startScale = transform.localScale;
        
    }

    public void FixedUpdate()
    {
        if (!squeezed)
        {
            Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
            Collider2D topCollider = CollisionHelper.getTopCollider(colliders);
            Collider2D bottomCollider = CollisionHelper.getBottomCollider(colliders);

            GameObject bottom = null;
            List<GameObject> sqeezerHits = CollisionHelper.getCollidingObject(bottomCollider, Enums.Edges.BOTTOM, 0.1f);
            foreach (GameObject hit in sqeezerHits)
            {
                if (hit != null)
                    bottom = hit;
            }
            
            GameObject top = null;
            sqeezerHits = CollisionHelper.getCollidingObject(topCollider, Enums.Edges.TOP, 0.1f);
            foreach (GameObject hit in sqeezerHits)
            {
                if (hit != null)
                    top = hit;
            }
            if (bottom != null && top != null)
            {
                sqeeezerScript = top.GetComponentInParent<SqeezerAnimationScript>();
                if (sqeeezerScript == null)
                    sqeeezerScript = bottom.GetComponentInParent<SqeezerAnimationScript>();
                if (sqeeezerScript != null && sqeeezerScript.isSqeezing && sqeeezerScript.lethal && sqeeezerScript.sqeezingDirection.Equals(Enums.Direction.VERTICAL))
                {
                    isYDirection = true;
                    mySize = Mathf.Abs(top.transform.position.y - bottom.transform.position.y);
                    squeezed = true;
                }
            }
            else
            {

                GameObject left = null;
                sqeezerHits = CollisionHelper.getCollidingObject(bottomCollider, Enums.Edges.LEFT, 0.1f);
                foreach (GameObject hit in sqeezerHits)
                {
                    if (hit != null)
                        left = hit;
                }

                GameObject right = null;
                sqeezerHits = CollisionHelper.getCollidingObject(bottomCollider, Enums.Edges.RIGHT, 0.1f);
                foreach (GameObject hit in sqeezerHits)
                {
                    if (hit != null)
                        right = hit;
                }

                if (left != null && right != null)
                {
                    sqeeezerScript = left.GetComponentInParent<SqeezerAnimationScript>();
                    if (sqeeezerScript == null)
                        sqeeezerScript = right.GetComponentInParent<SqeezerAnimationScript>();
                    if (sqeeezerScript != null && sqeeezerScript.isSqeezing && sqeeezerScript.lethal && sqeeezerScript.sqeezingDirection.Equals(Enums.Direction.HORIZONTAL))
                    {
                        isYDirection = false;
                        mySize = Mathf.Abs(right.transform.position.x - left.transform.position.x);
                        squeezed = true;
                    }
                }
            }
        }
        else
        {
            float speed = sqeeezerScript.distanceToMovePerFrame * Time.deltaTime;
            float percentageOfSize = speed/mySize;
            Vector3 myScale = transform.localScale;
            if (isYDirection)
            {
                myScale.y = myScale.y - (myScale.y * percentageOfSize * 2);
                transform.localScale = myScale;
                if (myScale.y <= (0.4 * startScale.y))
                {
                    HealthScript healthScript = GetComponent<HealthScript>();
                    //if (healthScript == null)
                    //{
                     //   healthScript = transform.parent.GetComponentInChildren<HealthScript>();
                    //}

                    if (healthScript != null)
                    {
                        healthScript.Die();
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }else
            {
                myScale.x = myScale.x - (myScale.x * percentageOfSize * 2);
                transform.localScale = myScale;
                if (myScale.x <= (0.4 * startScale.x))
                {
                    HealthScript healthScript = GetComponent<HealthScript>();
                    PartHealthScript partHealthScript = GetComponent<PartHealthScript>();


                    if (healthScript != null)
                    {
                        healthScript.Die();
                    }else if(partHealthScript != null )
                    {
                        partHealthScript.Die();
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
                
        }
    }
}
