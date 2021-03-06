//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using Assets.Scripts.Utils;
using System.Collections.Generic;
namespace AssemblyCSharp
{
		public class CollisionHelper
		{
			public static Enums.Edges getCollisionEdge(Collision2D collision)
			{
				
				ContactPoint2D contactPoint = collision.contacts [0];
				
				Collider2D thisCollider = contactPoint.otherCollider;
				
				float colliderEdgeRight = thisCollider.transform.position.x + thisCollider.bounds.extents.x - 0.1f;
				float colliderEdgeLeft = thisCollider.transform.position.x - thisCollider.bounds.extents.x + 0.1f;
				float colliderEdgeTop = thisCollider.transform.position.y + thisCollider.bounds.extents.y - 0.1f;
				float colliderEdgeBottom = thisCollider.transform.position.y - thisCollider.bounds.extents.y + 0.1f;
				

				if(colliderEdgeTop < contactPoint.point.y)
				{
					return Enums.Edges.TOP;
				}else if(colliderEdgeRight < contactPoint.point.x)
				{
					return Enums.Edges.RIGHT;
				}else if(colliderEdgeLeft > contactPoint.point.x)
				{
					return Enums.Edges.LEFT;
				}else if(colliderEdgeBottom > contactPoint.point.y)
				{
					return Enums.Edges.BOTTOM;
				}else
				{
					return  Enums.Edges.NONE;
				}
				

			}

            public static List<GameObject> getCollidingObject(Collider2D collider, Enums.Edges side, float distance)
            {
                if (collider != null)
                {
                    float distToGround = collider.bounds.extents.y;
                    float distToFront = collider.bounds.extents.x;
                    Vector3 bottomCenter = collider.transform.position;


                    RaycastHit2D hit1 = new RaycastHit2D();
                    RaycastHit2D hit2 = new RaycastHit2D();
                    RaycastHit2D hit3 = new RaycastHit2D();

                    int layer = 1 << 6;
                    layer += 1;
                    layer = layer << 2;
                    layer = ~layer;

                    if (Enums.Edges.BOTTOM.Equals(side))
                    {
                        hit1 = Physics2D.Raycast(bottomCenter + new Vector3(0, -(distToGround + 0.1f), 0), -Vector2.up, distance, layer);
                        hit2 = Physics2D.Raycast(bottomCenter + new Vector3(-(distToFront - 0.1f), -(distToGround + 0.1f), 0), -Vector2.up, distance, layer);
                        hit3 = Physics2D.Raycast(bottomCenter + new Vector3(distToFront - 0.1f, -(distToGround + 0.1f), 0), -Vector2.up, distance, layer);
                    }
                    else if (Enums.Edges.TOP.Equals(side))
                    {
                        hit1 = Physics2D.Raycast(bottomCenter + new Vector3(0, (distToGround + 0.1f), 0), Vector2.up, distance, layer);
                        hit2 = Physics2D.Raycast(bottomCenter + new Vector3(-(distToFront - 0.1f), (distToGround + 0.1f), 0), Vector2.up, distance, layer);
                        hit3 = Physics2D.Raycast(bottomCenter + new Vector3(distToFront - 0.1f, (distToGround + 0.1f), 0), Vector2.up, distance, layer);
                    }
                    else if (Enums.Edges.RIGHT.Equals(side))
                    {
                        hit1 = Physics2D.Raycast(bottomCenter + new Vector3(distToFront + 0.1f, 0, 0), Vector2.right, distance, layer);
                        hit2 = Physics2D.Raycast(bottomCenter + new Vector3(distToFront + 0.1f, -(distToGround / 2), 0), Vector2.right, distance, layer);
                        hit3 = Physics2D.Raycast(bottomCenter + new Vector3(distToFront + 0.1f, distToGround - 0.1f, 0), Vector2.right, distance, layer);
                    }
                    else if (Enums.Edges.LEFT.Equals(side))
                    {
                        hit1 = Physics2D.Raycast(bottomCenter + new Vector3(-(distToFront + 0.1f), 0, 0), -Vector2.right, distance, layer);
                        hit2 = Physics2D.Raycast(bottomCenter + new Vector3(-(distToFront + 0.1f), -(distToGround / 2), 0), -Vector2.right, distance, layer);
                        hit3 = Physics2D.Raycast(bottomCenter + new Vector3(-(distToFront + 0.1f), distToGround - 0.1f, 0), -Vector2.right, distance, layer);
                    }

                    List<GameObject> hits = new List<GameObject>();
                    if (hit1.collider != null)
                        hits.Add(hit1.collider.gameObject);
                    if (hit2.collider != null)
                        hits.Add(hit2.collider.gameObject);
                    if (hit3.collider != null)
                        hits.Add(hit3.collider.gameObject);
                    return hits;

                }
                return new List<GameObject>();
            }

        public static bool isCollision(Collider2D collider, Enums.Edges side, float distance)
        {
            List<GameObject> hits = getCollidingObject(collider, side, distance);
            bool iscollision = false;
            foreach (GameObject hit in hits)
            {
                if (hit != null)
                    iscollision |= true;
            }
            return iscollision;
        }
        public static Collider2D getBottomCollider(Collider2D[] colliders)
        {
                if (colliders.Length < 1)
                {
                    Debug.Log("no colliders found");
                    return null;
                }

                Collider2D bottomCollider = colliders[0];
                foreach (var aCollider in colliders)
                {
                    if (aCollider.transform.position.y < bottomCollider.transform.position.y)
                        bottomCollider = aCollider;
                }
                return bottomCollider;
        }
		
        public static Collider2D getTopCollider(Collider2D[] colliders)
        {
                if (colliders.Length < 1)
                {
                    Debug.Log("no colliders found");
                    return null;
                }

                Collider2D topCollider = colliders[0];
                foreach (var aCollider in colliders)
                {
                    if (aCollider.transform.position.y > topCollider.transform.position.y)
                        topCollider = aCollider;
                }
                return topCollider;
        }


		}
   
}

