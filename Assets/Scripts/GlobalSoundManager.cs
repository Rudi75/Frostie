using UnityEngine;
using System.Collections;

public class GlobalSoundManager : MonoBehaviour 
{
    public bool IsMusicEnabled { 
        get { return isMusicEnabled; }
        set { isMusicEnabled = value; update = true; } 
    }
    public bool AreEffectsEnabled { 
        get { return areEffectsEnabled; }
        set { areEffectsEnabled = value; update = true; } 
    }

    public bool update;
    public bool isMusicEnabled;
    public bool areEffectsEnabled;
    
    private static GlobalSoundManager Instance;

    void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

            update = false;
            isMusicEnabled = true;
            areEffectsEnabled = true;
        }
    }

	// Use this for initialization
	void Start () 
    {
        setAudioSettings();
	}

    void OnLevelWasLoaded(int level)
    {
        setAudioSettings();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (update)
        {
            update = false;
            setAudioSettings();
        }
	}

    private void setAudioSettings()
    {
        var audios = FindObjectsOfType<AudioSource>();
        foreach (var audio in audios)
        {
            audio.mute = !areEffectsEnabled;
        }
        var camera = FindObjectOfType<Camera>();
        var music = camera.gameObject.GetComponent<AudioSource>();
        music.mute = !isMusicEnabled;
    }

    public void ToggleGlobalSoundOnOff()
    {
        IsMusicEnabled = !IsMusicEnabled;
        AreEffectsEnabled = !AreEffectsEnabled;
    }
}
