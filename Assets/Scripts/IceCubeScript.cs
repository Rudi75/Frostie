using UnityEngine;
using System.Collections;

public class IceCubeScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lava"))
        {
            Destroy(gameObject);
        }
    }
}
