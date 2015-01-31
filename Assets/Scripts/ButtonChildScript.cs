using UnityEngine;
using System.Collections;

public class ButtonChildScript : MonoBehaviour {

    ButtonScript parent;
    void Start()
    {
        parent = GetComponentInParent<ButtonScript>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        parent.OnCollisionEnter2D(collision);
    }
}
