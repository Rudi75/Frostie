using UnityEngine;
using System.Collections;

public class StunningShotSkript : ShotScript
{
    public float StunningForce = 1;
    public float timeOfExistance = 20;

	// Use this for initialization
	void Start () 
    {
        Destroy(gameObject, timeOfExistance); // 20sec
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        var stunnable = other.gameObject.GetComponent<StunnableSkript>();
        if (stunnable == null)
            stunnable = other.gameObject.GetComponentInParent<StunnableSkript>();
        
        if (stunnable != null)
        {
            stunnable.Stunn(StunningForce);
            Destroy(gameObject);
        }
    }
}
