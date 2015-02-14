using UnityEngine;
using System.Collections;

public class FollowChild : MonoBehaviour {


    private Vector3 oldChildPosition = Vector3.zero;
    public Transform child;

    void FixedUpdate()
    {
        followChild();
    }

    private void followChild()
    {

        Vector3 movement = Vector3.zero;

        Vector3 childPosition = child.position;
        if (!Vector3.zero.Equals(oldChildPosition))
        {
            movement = childPosition - oldChildPosition;
        }
        oldChildPosition = childPosition;

        transform.Translate(movement, Space.World);
        child.Translate(-movement, Space.World);
    }
}
