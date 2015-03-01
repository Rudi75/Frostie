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

    // Use this for initialization
    void Start()
    {
        chainElementsContainer = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void InitializeObjects(Vector3 planksParentPosition, float lastPlankYPosition, DrawBridgePlanksScript.Directions openingDirection, float plankAngle, int planksCount)
    {
        float iterationCount = (90f / plankAngle)-1;
        float differenceToQuarterDegree = 45f - plankAngle;
        degreeChangePerIteration = differenceToQuarterDegree / iterationCount;
        currentChainAngle = plankAngle - degreeChangePerIteration;

        chainElementCountPerIteration = (planksCount + 1) / 3;
       
        InitializeChainBase(planksParentPosition, lastPlankYPosition);
        currentlyXChainPosition =-0.5f;
        UpdateChainObjects(true, openingDirection);
    }

    private void InitializeChainBase(Vector3 planksParentPosition, float lastPlankYPosition)
    {
        var drawBridgeChainBasePrefab = Instantiate(Resources.LoadAssetAtPath("Assets/Prefabs/Level/Engines/DrawbridgeChainBase.prefab", typeof(GameObject))) as GameObject;
        var iceBlockPrefab = Instantiate(Resources.LoadAssetAtPath("Assets/Prefabs/Level/Ice/IceEdgesAll.prefab", typeof(GameObject))) as GameObject;

        if (drawBridgeChainBasePrefab != null && iceBlockPrefab != null)
        {
            Transform drawBridgeChainBaseTransform = drawBridgeChainBasePrefab.transform;
            Transform iceBlockTransform = iceBlockPrefab.transform;

            if (drawBridgeChainBaseTransform != null && iceBlockTransform != null)
            {
                drawBridgeChainBaseTransform.position = new Vector3(planksParentPosition.x - 1.0f, lastPlankYPosition + 0.8f, planksParentPosition.z);
                drawBridgeChainBaseTransform.parent = iceBlockTransform.parent = transform;

                iceBlockTransform.position = new Vector3(planksParentPosition.x + 1.3f, drawBridgeChainBaseTransform.position.y+0.1f, drawBridgeChainBaseTransform.position.z);
                iceBlockTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                if (chainElementsContainer != null)
                {
                    chainElementsContainer.position = new Vector3(drawBridgeChainBaseTransform.position.x+0.5f, (drawBridgeChainBaseTransform.position.y), drawBridgeChainBaseTransform.position.z);
                }
            }
        } 
    }

    public void UpdateChainObjects(bool addChain, DrawBridgePlanksScript.Directions direction)
    {
        if (addChain)
        {
            for (int i = 0; i < chainElementCountPerIteration; i++)
            {
                var drawBridgeChainElementTransform = InstantiateNewChainPrefab();
                if (drawBridgeChainElementTransform != null)
                {
                    drawBridgeChainElementTransform.name = insertedChainElementCount.ToString();
                    drawBridgeChainElementTransform.localPosition = new Vector3(currentlyXChainPosition, 0.0f, 0.0f);

                    currentlyXChainPosition -= 2.2f;
                    insertedChainElementCount++;
                }
            }

            currentChainAngle += degreeChangePerIteration * Convert.ToInt32(direction);
            chainElementsContainer.rotation = Quaternion.AngleAxis(currentChainAngle, new Vector3(0.0f, 0.0f, 1f));
        }
        else
        {
            for (int i = 1; i <= chainElementCountPerIteration; i++)
            {
                Transform lastElement = chainElementsContainer.GetChild(insertedChainElementCount - 1);

                if (lastElement != null)
                {
                    DestroyPrefab(lastElement.gameObject);
                    currentlyXChainPosition += 2.2f;
                    insertedChainElementCount--;
                }
            }

            currentChainAngle -= degreeChangePerIteration;
            chainElementsContainer.rotation = Quaternion.AngleAxis(currentChainAngle, new Vector3(0.0f, 0.0f, 1f));
        }
    }

    private Transform InstantiateNewChainPrefab()
    {
        string chainPrefabName = insertedChainElementCount != 0 ? "ChainMiddle" : "ChainFront";
        var drawBridgeChainElement = Instantiate(Resources.LoadAssetAtPath("Assets/Prefabs/Level/Engines/"+chainPrefabName+".prefab", typeof(GameObject))) as GameObject;
       
        Transform drawBridgeChainElementTransform = null;

        if (drawBridgeChainElement != null)
        {
            drawBridgeChainElement.renderer.sortingOrder = 0;
            drawBridgeChainElementTransform = drawBridgeChainElement.transform;

            if (drawBridgeChainElementTransform != null)
            {
                drawBridgeChainElementTransform.parent = chainElementsContainer;
                drawBridgeChainElementTransform.localRotation = Quaternion.AngleAxis(0.0f, new Vector3(0.0f, 0.0f, 1f));
            }
        }

        return drawBridgeChainElementTransform;
    }

    private void DestroyPrefab(GameObject prefab)
    {
        Destroy(prefab);
    }
}
