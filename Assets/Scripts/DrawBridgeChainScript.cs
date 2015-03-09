using UnityEngine;
using System.Collections;
using System;

public class DrawBridgeChainScript : MonoBehaviour
{
    Transform chainElementsContainer;
    float currentChainAngle = 0.0f;
    float currentlyXChainPosition = 0.0f;

    int insertedChainElementCount = 0;
    int chainElementCountPerIteration = 0;
    float degreeChangePerIteration;

    DrawBridgePlanksScript.Directions openingDirection;
    public Sprite iceBlockTexture;
    private GameObject iceBlockPrefab;
    private GameObject drawBridgeChainBasePrefab;

    private int planksCount;
    Transform lastChainElement;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnDestroy()
    {
        //for (int i = chainElementsContainer.childCount - 1; i >= 0; i--)
        //{
        //    DestroyPrefab(chainElementsContainer.GetChild(i).gameObject);
        //}

        //chainElementsContainer = null;
        //iceBlockPrefab = null;
        //drawBridgeChainBasePrefab = null;
        //chainElementsContainer = null;
    }

    public void InitializeObjects(Vector3 planksParentPosition, float lastPlankYPosition, DrawBridgePlanksScript.Directions openingDirection, float currentAngle, int planksCount)
    {
        chainElementsContainer = transform.GetChild(0);
        iceBlockPrefab = transform.GetChild(1).gameObject;
        drawBridgeChainBasePrefab = transform.GetChild(2).gameObject;

        this.planksCount = planksCount;
        this.openingDirection = openingDirection;

        if (InitializeChainBase(lastPlankYPosition))
        {
            InitializeAngleValues();
            InitializeChains(currentAngle);
            
        }
        else
            Debug.LogError("Could not initialize ChainBase!");
    }

    private bool InitializeChains(float currentAngle)
    {
        if (chainElementsContainer != null)
        {
            chainElementsContainer.localPosition = new Vector3(0.0f, drawBridgeChainBasePrefab.transform.localPosition.y, 0.0f);
            lastChainElement = chainElementsContainer.GetChild(0);

            if (lastChainElement != null)
            {
                currentlyXChainPosition = openingDirection == DrawBridgePlanksScript.Directions.right ? 1.2f : -1.2f;

                if(planksCount+1==12)
                {
                    currentlyXChainPosition = openingDirection == DrawBridgePlanksScript.Directions.right ? 2f : -2f;
                }

                lastChainElement.localPosition = new Vector3(currentlyXChainPosition, 0.0f, -2.0f);

                //Debug.Log("planksCount: " + planksCount);
                chainElementCountPerIteration = (planksCount + 1) / 3;
                insertedChainElementCount = 1;
                //chainElementCountPerIteration -= 1;

                for (int i = 1; (i * 22.5f) <= Math.Abs(currentAngle); i++)
                {
                    if(i==1)
                        chainElementCountPerIteration -= 1;
                    else
                        chainElementCountPerIteration = (planksCount + 1) / 3;

                    //Debug.Log("current temp angle: " + (i * 22.5f));
                    //Debug.Log("Math.Abs(currentAngle): " + Math.Abs(currentAngle));
                    UpdateChainObjects(true, openingDirection);
                }

                chainElementCountPerIteration = (planksCount + 1) / 3;

                return true;
            }
            Debug.LogError("lastChain Element is null!");
        }
        else
            Debug.LogError("chainElementsContainer is null!");

        return false;
    }

    private void InitializeAngleValues()
    {
        float iterationCount = (90f / 22.5f) - 1;
        float differenceToQuarterDegree = 45f - 22.5f;
        degreeChangePerIteration = differenceToQuarterDegree / iterationCount;
        currentChainAngle = ((Convert.ToInt32(openingDirection)) * 22.5f) - ((Convert.ToInt32(openingDirection)) * degreeChangePerIteration);
        //Debug.Log("currentChainAngle: " + currentChainAngle);
    }


    private bool InitializeChainBase(float lastPlankYPosition)
    {
        if (drawBridgeChainBasePrefab != null)
        {
            Transform drawBridgeChainBaseTransform = drawBridgeChainBasePrefab.transform;

            if (drawBridgeChainBaseTransform != null)
            {
                //Debug.Log("lastPlankYPosition: " + lastPlankYPosition);
                float xOffset = -1.0f;
                if(planksCount+1==12)
                {
                    Debug.Log("PlanksCount is 12!!");
                    xOffset=-1.5f;
                }

                drawBridgeChainBaseTransform.localPosition = new Vector3(xOffset, lastPlankYPosition + 0.5f, 2.0f);
                InitializeIceBlockPrefab(drawBridgeChainBaseTransform);
				return true;
            }
            Debug.LogError("drawBridgeChainBaseTransform is null!");
        }
        Debug.LogError("drawBridgeChainBasePrefab is null!");
        return false;
    }

    private bool InitializeIceBlockPrefab(Transform drawBridgeChainBaseTransform)
    {
        if (drawBridgeChainBaseTransform != null && ChangeTextureOfPrefab(iceBlockPrefab, iceBlockTexture))
        {
            Transform iceBlockTransform = iceBlockPrefab.transform;

            if (iceBlockTransform != null)
            {
                float iceBlockXPositionOffset = openingDirection == DrawBridgePlanksScript.Directions.right ? -0.5f : +2.25f;
                iceBlockTransform.localPosition = new Vector3(drawBridgeChainBaseTransform.localPosition.x + iceBlockXPositionOffset, drawBridgeChainBaseTransform.localPosition.y, drawBridgeChainBaseTransform.localPosition.z);
                return true;
            }
            else
                Debug.LogError("iceBlockTransorm is null!");
        }
        return false;
    }

    private bool ChangeTextureOfPrefab(GameObject sourceGameObject, Sprite newTexture)
    {
        if (sourceGameObject != null && newTexture != null) {
			SpriteRenderer renderer = sourceGameObject.GetComponent<SpriteRenderer> ();
			if (renderer != null) {
				renderer.sprite = newTexture;
				return true;
			} else
				Debug.LogError ("Could not find SpriteRenderer for Gameobject: " + sourceGameObject.name);
		}
		else
		{
			//Debug.LogError ("Could not change Sprite for GameObject!");
		}

        return false;
    }

    public void UpdateChainObjects(bool addChain, DrawBridgePlanksScript.Directions direction)
    {
        //Debug.Log("In Function UpdateChainObjects!");
        //Debug.Log("chainElementCountPerIteration: " + chainElementCountPerIteration);

        if (addChain)
        {
            for (int i = 0; i < chainElementCountPerIteration; i++)
            {
                if (InstantiateNewChainPrefab())
                    insertedChainElementCount++;
            }

            currentChainAngle = currentChainAngle + degreeChangePerIteration * Convert.ToInt32(direction);
            if (chainElementsContainer != null)
                chainElementsContainer.rotation = Quaternion.AngleAxis(currentChainAngle, new Vector3(0.0f, 0.0f, 1f));
        }
        else
        {
            //Debug.Log("chainElementCountPerIteration: " + chainElementCountPerIteration);
            for (int i = 1; i <= chainElementCountPerIteration; i++)
            {
                if (lastChainElement != null)
                {
                    DestroyPrefab(lastChainElement.gameObject);
                    currentlyXChainPosition = openingDirection == DrawBridgePlanksScript.Directions.left ? currentlyXChainPosition + 2.2f : currentlyXChainPosition - 2.2f;
                    insertedChainElementCount--;
                }

                lastChainElement = chainElementsContainer.GetChild(insertedChainElementCount - 1);
            }

            currentChainAngle -= degreeChangePerIteration * Convert.ToInt32(openingDirection);
            chainElementsContainer.rotation = Quaternion.AngleAxis(currentChainAngle, new Vector3(0.0f, 0.0f, 1f));
        }
    }

    private bool InstantiateNewChainPrefab()
    {
        string chainPrefabName = insertedChainElementCount != 0 ? " drawbridge_chain_middle" : " drawbridge_chain_front";
       // var drawBridgeChainElement = Instantiate(Resources.LoadAssetAtPath("Assets/Prefabs/Level/Engines/"+chainPrefabName+".prefab", typeof(GameObject))) as GameObject;

        if (lastChainElement != null)
        {
            var newItem = Instantiate(lastChainElement) as Transform;

            if (newItem != null)
            {
                Sprite chainSprite = Resources.LoadAssetAtPath("Assets/Textures/Buttons & Engines & More/" + chainPrefabName + ".png", typeof(Sprite)) as Sprite;

                if (chainSprite != null)
                {
                    if (ChangeTextureOfPrefab(newItem.gameObject, chainSprite))
                    {
                        newItem.parent = lastChainElement.parent;
                        newItem.localPosition = new Vector3(lastChainElement.localPosition.x + (2.25f * Convert.ToInt32(openingDirection)*-1.0f), 0.0f, 0.0f);
                        newItem.localRotation = Quaternion.AngleAxis(currentChainAngle, new Vector3(0.0f, 0.0f, 0.0f));
                        lastChainElement = newItem;

                        return true;
                    }
                }
                Debug.LogError("Could no load sprite: " + chainPrefabName);
            }
        }
        Debug.LogError("Could not create new ChainElement cause lastChainElement is null!");
        return false;

        //Transform drawBridgeChainElementTransform = null;

        //if (drawBridgeChainElement != null)
        //{
        //    drawBridgeChainElement.renderer.sortingOrder = 0;
        //    drawBridgeChainElementTransform = drawBridgeChainElement.transform;

        //    if (drawBridgeChainElementTransform != null)
        //    {
        //        drawBridgeChainElementTransform.SetParent(chainElementsContainer);
        //        drawBridgeChainElementTransform.localRotation = Quaternion.AngleAxis(0.0f, new Vector3(0.0f, 0.0f, 1f));
        //        Vector3 position = drawBridgeChainElementTransform.localPosition;
        //        position.z = -0.1f;
        //        drawBridgeChainElementTransform.localPosition = position;
        //    }
        //}

        //return drawBridgeChainElementTransform;
    }

    private void DestroyPrefab(GameObject prefab)
    {
        Destroy(prefab);
    }

    
    public void RemoveCompleteChain()
    {
        if(chainElementsContainer!=null)
        {
            for(int i=chainElementsContainer.childCount-1;i>=0;i--)
            {
                Transform currentChainElement = chainElementsContainer.GetChild(i);
                if (currentChainElement != null && currentChainElement.gameObject != null)
                    Destroy(currentChainElement.gameObject);
            }
        }
    }
}
