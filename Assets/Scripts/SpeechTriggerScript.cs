using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechTriggerScript : TargetActionScript {

    public string speech;
    public bool activated = true;
    public bool triggered = false;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Contains("Player") && activated && !triggered)
        {
            
            SpeechBubble bubble = other.gameObject.GetComponentInParent<SpeechBubble>();
            if(bubble != null)
            {
                bubble.speak(speech);
                this.triggered = true;
               // bubble.enabled = true;

                foreach (SpeechTriggerScript trigger in speechTriggerToActivate)
                {
                    trigger.activateTrigger();
                }

                foreach (SpeechTriggerScript trigger in speechTriggerToDeactivate)
                {
                    trigger.deactivateTrigger();
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
                //bubble.setSpeech(" ");
                bubble.enabled = false;
                //Destroy(gameObject);
                
            }
        }
    }
    protected override void performAction()
    {
        activateTrigger();
    }

    public void activateTrigger()
    {
        activated = true;
    }

    public void deactivateTrigger()
    {
        activated = false;
    }
}
