using UnityEngine;
using System.Collections;

public class SpeechTriggerScript : MonoBehaviour {

    public string speech;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Player"))
        {
            SpeechBubble bubble = other.gameObject.GetComponentInParent<SpeechBubble>();
            if(bubble != null)
            {
                bubble.setSpeech(speech);
                bubble.enabled = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Contains("Player"))
        {
            SpeechBubble bubble = other.gameObject.GetComponentInParent<SpeechBubble>();
            if (bubble != null)
            {
                bubble.setSpeech(" ");
                bubble.enabled = false;
            }
        }
    }

}
