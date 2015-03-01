using UnityEngine;
using System.Collections;
using Mood = Assets.Scripts.Utils.Enums.Mood;

public class MoodScript : MonoBehaviour 
{
    public Transform Nose;
    public Transform MouthAngry;
    public Transform MouthHappy;
    public Transform MouthSad;

    public Transform EyeBrowLeft;
    public Transform EyeBrowRight;

    public void SetMood(Mood mood)
    {
        switch (mood)
        {
            case Mood.SAD:
                Nose.gameObject.SetActive(false);
                MouthAngry.gameObject.SetActive(false);
                MouthHappy.gameObject.SetActive(false);
                MouthSad.gameObject.SetActive(true);
                setEyeBrowsActive(false);
                break;
            case Mood.HAPPY:
                Nose.gameObject.SetActive(true);
                MouthAngry.gameObject.SetActive(false);
                MouthHappy.gameObject.SetActive(true);
                MouthSad.gameObject.SetActive(false);
                setEyeBrowsActive(false);
                break;
            case Mood.ANGRY:
                Nose.gameObject.SetActive(false);
                MouthAngry.gameObject.SetActive(true);
                MouthHappy.gameObject.SetActive(false);
                MouthSad.gameObject.SetActive(false);
                setEyeBrowsActive(true);
                break;
        }
    }

    private void setEyeBrowsActive(bool active)
    {
        if (EyeBrowLeft != null)
            EyeBrowLeft.gameObject.SetActive(active);
        if (EyeBrowRight != null)
            EyeBrowRight.gameObject.SetActive(active);
    }
}
