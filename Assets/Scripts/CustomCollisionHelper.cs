using System;
using UnityEngine;
using Assets.Scripts.Utils;
using System.Collections.Generic;

public class CustomCollisionHelper
{
    public static Collider2D getBiggestCollider(Collider2D[] colliders)
    {
        if (colliders.Length < 1)
        {
            return null;
        }

        Collider2D collider = colliders[0];
        foreach (var other in colliders)
        {
            Vector3 sizeOther = other.bounds.size;
            Vector3 sizeThis = collider.bounds.size;
            float planeOther = sizeOther.x * sizeOther.y;
            float planeThis = sizeThis.x * sizeThis.y;
            if (planeOther > planeThis)
                collider = other;
        }
        return collider;
    }

    public static Collider2D getLeftCollider(Collider2D[] colliders)
    {
        if (colliders.Length < 1)
        {
            throw new System.Exception("no colliders found");
        }

        Collider2D leftCollider = colliders[0];
        foreach (var aCollider in colliders)
        {
            if (aCollider.transform.position.x < leftCollider.transform.position.x)
                leftCollider = aCollider;
        }
        return leftCollider;
    }

    public static Collider2D getRigthCollider(Collider2D[] colliders)
    {
        if (colliders.Length < 1)
        {
            throw new System.Exception("no colliders found");
        }

        Collider2D rightCollider = colliders[0];
        foreach (var aCollider in colliders)
        {
            if (aCollider.transform.position.x > rightCollider.transform.position.x)
                rightCollider = aCollider;
        }
        return rightCollider;
    }

    public static IEnumerable<GameObject> getCollidingObjectsInBetween(Collider2D begin, Collider2D end, Enums.Direction side)
    {
        Debug.Log("getCollidingObjectsInBetween");

        Vector3 first = begin.transform.position;
        Vector3 second = end.transform.position;

        if (side == Enums.Direction.HORIZONTAL)
        {
            first.x -= begin.bounds.extents.x;
            second.x += end.bounds.extents.x;
        }
        if (side == Enums.Direction.VERTICAL)
        {
            first.y += begin.bounds.extents.y;
            second.y -= end.bounds.extents.y;
        }

        Vector3 direction = second - first;
        var collisions = new List<GameObject>();

        RaycastHit2D[] hits = Physics2D.RaycastAll(first, Vector3.Normalize(direction), Vector3.Magnitude(direction));
        foreach (RaycastHit2D hit in hits)
        {
            collisions.Add(hit.collider.gameObject);
        }

        return collisions;
    }

    public static IEnumerable<GameObject> getCollidingObjects(Collider2D collider, Enums.Edges side, float distance)
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
            hit2 = Physics2D.Raycast(bottomCenter + new Vector3(distToFront + 0.1f, -(distToGround - 0.1f), 0), Vector2.right, distance, layer);
            hit3 = Physics2D.Raycast(bottomCenter + new Vector3(distToFront + 0.1f, distToGround - 0.1f, 0), Vector2.right, distance, layer);
        }
        else if (Enums.Edges.LEFT.Equals(side))
        {
            hit1 = Physics2D.Raycast(bottomCenter + new Vector3(-(distToFront + 0.1f), 0, 0), -Vector2.right, distance, layer);
            hit2 = Physics2D.Raycast(bottomCenter + new Vector3(-(distToFront + 0.1f), -(distToGround - 0.1f), 0), -Vector2.right, distance, layer);
            hit3 = Physics2D.Raycast(bottomCenter + new Vector3(-(distToFront + 0.1f), distToGround - 0.1f, 0), -Vector2.right, distance, layer);
        }

        var collisions = new List<GameObject>();
        if (hit1.collider != null)
        {
            collisions.Add(hit1.collider.gameObject);
        }
        else if (hit2.collider != null)
        {
            collisions.Add(hit2.collider.gameObject);
        }
        else if (hit3.collider != null)
        {
            collisions.Add(hit3.collider.gameObject);
        }

        return collisions;
    }
}

