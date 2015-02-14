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

        Collider2D bottomCollider = CollisionHelper.getBotomCollider(colliders);

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
