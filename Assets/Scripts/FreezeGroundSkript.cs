using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts.Utils;

public class FreezeGroundSkript : MonoBehaviour 
{
    public KeyCode KeyToFreeze = KeyCode.G;

    private Collider2D bottomCollider;
	// Use this for initialization
	void Start () 
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        bottomCollider = CollisionHelper.getBotomCollider(colliders);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyToFreeze))
        {
            Debug.Log("grgr");
            var ground = CollisionHelper.getCollidingObject(bottomCollider, Enums.Edges.BOTTOM, 0.1f);
            var freezableGround = ground.GetComponent<FreezableGround>();
            if (freezableGround == null) freezableGround = ground.transform.parent.GetComponent<FreezableGround>();

            if (freezableGround != null)
            {
                Debug.Log("babababa");
                freezableGround.Freeze();
            }
        }
	}
}
