using UnityEngine;
using System.Collections;

public class ButtonShotManager : MonoBehaviour 
{
    private SpriteRenderer button;

    public bool WasNotShotJet
    {
        get { return button.enabled; }
    }

	// Use this for initialization
	void Start () 
    {
        button = GetComponent<SpriteRenderer>();
	}

    public void WasShot()
    {
        button.enabled = false;
    }

    public void Reset()
    {
        button.enabled = true;
    }
}
