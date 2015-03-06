using UnityEngine;
using System.Collections;

public class SoundOnOffScript : MonoBehaviour 
{
    private GlobalSoundManager soundManager;

    private GameObject On;
    private GameObject Off;

	// Use this for initialization
	void Start () 
    {
        soundManager = FindObjectOfType<GlobalSoundManager>();
        On = transform.parent.FindChild("TextOn").gameObject;
        Off = transform.parent.FindChild("TextOff").gameObject;
	}

    public void ToggleButtonPressed()
    {
        soundManager.ToggleGlobalSoundOnOff();
        On.SetActive(!On.activeSelf);
        Off.SetActive(!Off.activeSelf);
    }
}
