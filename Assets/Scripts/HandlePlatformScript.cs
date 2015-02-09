using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class HandlePlatformScript : MonoBehaviour {


    private Vector3 oldPlatformPosition = Vector3.zero;

    void FixedUpdate()
    {
        handleMovingPlatforms();
    }

    private void handleMovingPlatforms()
    {
        Collider2D[] colliders =  GetComponentsInChildren<Collider2D>();
        if(colliders.Length < 1)
        {
            throw new System.Exception("no colliders found");
        }
        Collider2D bottomCollider = colliders[0];
        foreach (var aCollider in colliders)
        {
            if (aCollider.transform.position.y < bottomCollider.transform.position.y)
                bottomCollider = aCollider;
        }

        Vector3 movement = Vector3.zero;
        GameObject platform = CollisionHelper.getCollidingObject(bottomCollider, Edges.BOTTOM, 0.1f);

        if (platform == null || platform.tag != "Platform")
        {
            oldPlatformPosition = Vector3.zero;
            return;
        }
        Vector3 platformPosition = platform.transform.position;
        if (!Vector3.zero.Equals(oldPlatformPosition))
        {
            movement = platformPosition - oldPlatformPosition;
        }
        oldPlatformPosition = platformPosition;

        transform.Translate(movement, Space.World);
    }
}
