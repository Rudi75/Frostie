using UnityEngine;
using System.Collections;
using System;

public class DrawBridgePlanksScript : MonoBehaviour {

    public enum Directions
    {
        left = 1,
        right = -1
    };

    public Directions openingDirection;
    public DrawBridgeChainScript ChainScript;
    public int planksCount;

    private float startingAngle = 22.5f;
    private float currentAngle = 22.5f;

	// Use this for initialization
    void Start()
    {
        if (planksCount < 3)
            planksCount = 3;
        else
        {
            float tempCount = planksCount;
            int modulo = planksCount % 3;

            while (modulo !=0)
            {
                planksCount++;
                modulo = planksCount % 3;
            }
        }

        transform.localScale = new Vector3(transform.localScale.x * Convert.ToInt32(openingDirection), transform.localScale.y, transform.localScale.z);

        Transform child = transform.GetChild(0);
        planksCount -= 1;

        for (int i = 0; i < planksCount; i++)
        {
            var newItem = Instantiate(child) as Transform;

            newItem.position = new Vector3(child.position.x, child.position.y + 2.25f, child.position.z);
            newItem.parent = transform;
            child = newItem;
        }

        transform.rotation = Quaternion.AngleAxis(currentAngle, new Vector3(0.0f, 0.0f, 1f));

        if (ChainScript != null)
            ChainScript.InitializeObjects(transform.position, child.position.y, openingDirection, currentAngle,  planksCount);    
    }  
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.rotation = Quaternion.AngleAxis(currentAngle, new Vector3(0.0f, 0.0f, 1f));
	}

    public void rotateOnce(Directions direction)
    {
        float tempAngle = currentAngle + (Convert.ToInt32(direction) * 22.5f);

        if (openingDirection == Directions.left)
        {
            if (tempAngle <= startingAngle)
                tempAngle = startingAngle;

            if (tempAngle >= 90)
                tempAngle = 90;
        }
        else
        {
            if (tempAngle >= startingAngle)
                tempAngle = startingAngle;

            if (tempAngle <= -90)
                tempAngle = -90;
        }

        if (currentAngle != tempAngle && ChainScript != null)
        {
            currentAngle = tempAngle;
            transform.rotation = Quaternion.AngleAxis(currentAngle, new Vector3(0.0f, 0.0f, 1f));
            ChainScript.UpdateChainObjects(direction == openingDirection, direction);
        }
    }

    public bool IsBridgeLockedInDirection(Directions direction)
    {      
        float tempAngle = currentAngle + (Convert.ToInt32(direction) * 22.5f);

        if (openingDirection == Directions.left)
        {
            if (tempAngle < startingAngle || tempAngle > 90)
                return true;
        }
        else
        {
            if (tempAngle > startingAngle || tempAngle < -90)
                return true;
        }

        return false;
    }
}
