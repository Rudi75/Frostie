using UnityEngine;
using System.Collections;

public class ImpaleScript : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        HealthScript livingBeeing = collider.gameObject.GetComponentInParent<HealthScript>();
        if (livingBeeing != null)
        {
            var Audio = GetComponentInParent<AudioSource>();
            if (Audio == null) Audio = GetComponent<AudioSource>();
            if(!Audio.isPlaying)
                Audio.Play();
            return;
        }
    }
}
