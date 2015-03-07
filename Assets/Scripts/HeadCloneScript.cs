using UnityEngine;
using System.Collections;

public class HeadCloneScript : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if(rigidbody2D.velocity.Equals(Vector2.zero))
        {
            FrostiePartManager frostiePartManager = transform.parent.GetComponentInChildren<FrostiePartManager>();
            frostiePartManager.recallHead();
        }	
	}
}
