using UnityEngine;
using System.Collections;

public class DecoupleMiddleScript : MonoBehaviour {

    private FrostiePartManager partManager;
    public int throwForce = 15;

    public void Start()
    {
        partManager = GetComponentInParent<FrostiePartManager>();
    }

    public void decouple()
    {
        if (partManager.getHeadAndMiddleClone() == null && partManager.getMiddlePartClone() == null)
        {
            GameObject middlePart = partManager.decoupleMiddlePart();

            middlePart.rigidbody2D.AddForce(new Vector2(1, 1) * throwForce, ForceMode2D.Impulse);
        }
    }
}
