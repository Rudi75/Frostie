using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour
{
    public int damage = 1;
    public bool isEnemyShot = true;

    // Use this for initialization
    void Start()
    {
        // Limited time to live to avoid any leak
        Destroy(gameObject, 20); // 20sec
    }

    // Update is called once per frame
    void Update()
    {

    }
}
