using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechTriggerScript : TargetActionScript {

    public string speech;
    public bool activated = true;
    public bool triggered = false;
    public List<SpeechTriggerScript> speechTriggerToActivate;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Player") && activated && !triggered)
        {
            SpeechBubble bubble = other.gameObject.GetComponentInParent<SpeechBubble>();
            if(bubble != null)
            {
                bubble.setSpeech(speech);
                bubble.enabled = true;

                foreach (SpeechTriggerScript trigger in speechTriggerToActivate)
                {
                    trigger.performAction();
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Contains("Player") && activated)
        {
            SpeechBubble bubble = other.gameObject.GetComponentInParent<SpeechBubble>();
            if (bubble != null)
            {
                bubble.setSpeech(" ");
                bubble.enabled = false;
                //Destroy(gameObject);
                this.triggered = true;
            }
        }
    }
    protected override void performAction()
    {
        activated = true;
    }
}
