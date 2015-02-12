using UnityEngine;
using System.Collections;

public class SqeezerAnimationScript : MonoBehaviour
{


    private Transform top;
    private Transform bottom;
    private Transform arm;
    private Transform squeezer;
    private Vector3 startPosition;
    public float direction = -1;

    private float timeTillMove;

    public float distanceToMovePerFrame = 1;
    public float timeToWait = 1;
	// Use this for initialization
	void Start () {

        foreach (Transform child in transform)
        {
            if(child.name.Contains("Top"))
                top = child;
            else if(child.name.Contains("Base"))
                bottom = child;
            else if(child.name.Contains("Arm"))
                arm = child;
            else
                squeezer = child; 
        }
        startPosition = squeezer.position;
        timeTillMove = timeToWait;
	}

	
	// Update is called once per frame
	void FixedUpdate () {
        timeTillMove -= Time.deltaTime;

        if (squeezer.position.y >= startPosition.y && direction == 1)
        {
            direction = -1;
            timeTillMove = timeToWait;
        }
            
        if (squeezer.position.y <= bottom.position.y && direction == -1)
        {
            direction = 1;
            timeTillMove = timeToWait;
        }

        if (timeTillMove <= 0)
        {
            squeezer.Translate(new Vector3(0, direction * distanceToMovePerFrame, 0), Space.World);
            Vector3 armScale = arm.localScale;
            armScale.y = armScale.y - (2* direction * distanceToMovePerFrame);
            arm.localScale = armScale;
        }
	}
}
