using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class HeadCloneScript : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

        GameObject collidingObject = CustomCollisionHelper.getCollidingObject(collider2D, Assets.Scripts.Utils.Enums.Edges.BOTTOM, 0.1f);
        if(rigidbody2D.velocity.Equals(Vector2.zero) && collidingObject != null)
        {
            FrostiePartManager frostiePartManager = transform.parent.GetComponentInChildren<FrostiePartManager>();
            frostiePartManager.recallHead();
        }	
	}
}
