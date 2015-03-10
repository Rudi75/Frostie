using UnityEngine;
using System.Collections;

public class StorySoundCallback : MonoBehaviour 
{
    public void StartSound()
    {
        var cam = GetComponentInChildren<Camera>();
        if (cam != null)
        {
            cam.audio.Play();
        }
    }
}
