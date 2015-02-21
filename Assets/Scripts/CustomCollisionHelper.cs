using System;
using UnityEngine;

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
}

