using UnityEngine; // 41 Post - Created by DimasTheDriver on Dec/12/2011 . Part of the 'Unity: How to create a speech balloon' post. Available at: http://www.41post.com/?p=4545 
using System;

[ExecuteInEditMode]
public class SpeechBubble : MonoBehaviour 
{
	//this game object's transform
	private Transform goTransform;
	//the game object's position on the screen, in pixels
	private Vector3 goScreenPos;
	
	//a guiSkin, to render the round part of the speech balloon
	public GUISkin guiSkin;
    public string speech = "hello";
	
    public int OffsetXStart = -50;
    public int OffsetYStart = 100;

	//use this for early initialization
	void Awake() 
	{
		//get this game object's transform
		goTransform = this.GetComponent<Transform>();
	}
	
	//use this for initialization
	void Start()
	{
		
		//if the guiSkin hasn't been found
		if (!guiSkin) 
		{
			Debug.LogError("Please assign a GUI Skin on the Inspector.");
			return;
		}
	}
	
	public void setSpeech(string speech_)
    {
        this.speech = speech_;
    }

	//Called once per frame, after the update
	void LateUpdate() 
	{
		//find out the position on the screen of this game object
		goScreenPos = Camera.main.WorldToScreenPoint(goTransform.position);	
	}
	
	//Draw GUIs
	void OnGUI()
	{
        int x = speech.Length;
        float potensator = 1.2f;

        if(x > 30)
        {
            int temp = x / 30;
            potensator = 1.1f + (float)temp/10;
        }
        int bubbleWidth = (int)(Math.Log(Math.Pow((double)x, potensator)) * 40);
        int bubbleHeight = (int)(Math.Log(Math.Pow((double)x, potensator)) * 20);
        int centerOffsetX = bubbleWidth / 2;
        int centerOffsetY = bubbleHeight / 2;
        int offsetX = OffsetXStart - (bubbleWidth - 40) / 2;
        int offsetY = OffsetYStart + (bubbleHeight - 40) / 2;
		//Begin the GUI group centering the speech bubble at the same position of this game object. After that, apply the offset
		GUI.BeginGroup(new Rect(goScreenPos.x-centerOffsetX-offsetX,Screen.height-goScreenPos.y-centerOffsetY-offsetY,bubbleWidth,bubbleHeight));
			
			//Render the round part of the bubble
        GUI.Label(new Rect(0, 0, bubbleWidth, bubbleHeight), "", guiSkin.customStyles[0]);

			//Render the text
        float startLeft = bubbleWidth * 0.1f;
        float startTop = bubbleHeight * 0.1f;
        GUI.Label(new Rect(startLeft, startTop, bubbleWidth - (2 * startLeft), bubbleHeight - (2 * startTop)), speech, guiSkin.label);
		
		GUI.EndGroup();
	}
}