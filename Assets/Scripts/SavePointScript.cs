using UnityEngine;
using System.Collections;

public class SavePointScript : MonoBehaviour {
   
    private bool activated = false;
    private Transform fire;

    public void Awake()
    {
        foreach (Transform childTransform in transform)
        {
            if(childTransform.name.Contains("Fire"))
            {
                fire = childTransform;
            }
        }
    }
public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("servas");
        if (other.tag.Contains("Player"))
        {
            activated = true;
            fire.gameObject.SetActive(true);
            FrostiePartManager.spawnPosition = transform.position;
        }
    }
}
