using UnityEngine;
using System.Collections;
using Faces = Assets.Scripts.Utils.Enums.Faces;
using Mood = Assets.Scripts.Utils.Enums.Mood;

public class LevelEndTriggerScript : MonoBehaviour
{
    public string Level = "LevelMenu";
    public float DelayLoadLevel = 5.0f;
    public string Speech;
    public Mood MoodeToGetIn;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Player"))
        {
            var bubble = other.gameObject.GetComponentInParent<SpeechBubble>();
            if(bubble != null)
            {
                bubble.speak(Speech);
                //bubble.enabled = true;
            }

            var animator = GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.enabled = true;
            }

            var status = other.gameObject.GetComponentInParent<FrostieStatus>();
            if (status != null)
            {
                status.isFixated = true;
                status.IsFaceing = Faces.FRONT;
                status.IsInMood = MoodeToGetIn;
            }

            StartCoroutine(LevelEnd());
        }
    }

    private IEnumerator LevelEnd()
    {
        yield return new WaitForSeconds(DelayLoadLevel);
        Time.timeScale = 1;
        Application.LoadLevel(Level);
    }
}
