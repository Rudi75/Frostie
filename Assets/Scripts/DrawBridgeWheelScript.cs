using UnityEngine;
using System.Collections;

public class DrawBridgeWheelScript : MonoBehaviour {

    private FrostieScript frostie;
    private bool wheelInUse = false;

    private DrawBridgePlanksScript PlanksReference;
    public Transform plankTransform;

	// Use this for initialization
	void Start () {
        PlanksReference = plankTransform.GetComponent<DrawBridgePlanksScript>();

        if (PlanksReference != null)
            Debug.Log("Found Planks!");
	}
	
	// Update is called once per frame
    void Update()
    {
        if (frostie != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (wheelInUse)
                {                    
                    frostie.ReleaseFixedPosition();
                    wheelInUse = false;
                    Debug.Log("Wheel is no more in use!");
                }
                else
                {
                    frostie.FixatePosition();
                    wheelInUse = true;
                    Debug.Log("Wheel is now in use!");
                }
            }

            if (wheelInUse)
            {
                //Debug.Log("Wheel is in use!");

                if (PlanksReference != null)
                {
                    Debug.Log("Found the planks!");

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        PlanksReference.rotateOnce(DrawBridgePlanksScript.Directions.left);
                    }

                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        PlanksReference.rotateOnce(DrawBridgePlanksScript.Directions.right);
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Reached the wheel!");
        frostie = other.gameObject.GetComponentInParent<FrostieScript>();
       
    }
}
