using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class GifLikeAnimation : MonoBehaviour 
{
    public float animationRate = 0.0f;

    private float timeToNextFrame;

    private int currentID;
    private SortedList<int, SpriteRenderer> images;

    void Awake()
    {
        images = new SortedList<int, SpriteRenderer>();
    }

	// Use this for initialization
	void Start () 
    {
        timeToNextFrame = 0;
        currentID = 0;

        foreach (Transform child in transform)
        {
            SpriteRenderer childRenderer = child.renderer as SpriteRenderer;
            if (childRenderer != null)
            {
                childRenderer.enabled = false;
                images.Add(currentID, childRenderer);
                currentID++;
            }
        }

        currentID = 0;
        images.ElementAt(currentID).Value.enabled = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (timeToNextFrame > 0)
        {
            timeToNextFrame -= Time.deltaTime;
        }
        else
        {
            //Switch frame
            images.ElementAt(currentID).Value.enabled = false;
            currentID++;
            currentID = currentID % images.Count;
            images.ElementAt(currentID).Value.enabled = true;

            timeToNextFrame = animationRate;
        }
	}


}
