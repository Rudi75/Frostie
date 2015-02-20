using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour
{
    public int damage = 1;
    public bool isEnemyShot = true;
    public bool destroyShotAtCollision = true;

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

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Destroy(gameObject);        
        if (destroyShotAtCollision && !collider.gameObject.tag.Contains("Lethal") && !collider.isTrigger)
        {
            gameObject.renderer.enabled = false;
        }
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        //Destroy(gameObject);        
        if (destroyShotAtCollision && !collider.gameObject.tag.Contains("Lethal") && !collider.isTrigger)
        {
            Destroy(gameObject); 
        }
    }
   
}
