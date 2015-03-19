using UnityEngine;
using System.Collections;

public class DrawBridgeScript : MonoBehaviour 
{
    public float Angle = 75.0f;

    public float MinAngle = 0.0f;
    public float MaxAngle = 75.0f;
    
    private Transform Chain;
    private Transform Plank;

    private Vector2 PlanksTopPos;
    private Vector2 PlanksBasePos;
    private Vector2 ChainBasePos;

	// Use this for initialization
	void Start () 
    {
        Transform chainBase = transform.FindChild("DrawbridgeChainBase");
        Plank = transform.FindChild("DrawbridgePlank");
        Chain = Plank.FindChild("DrawbridgeChain");

        PlanksBasePos = new Vector2(Plank.position.x, Plank.position.y);
        ChainBasePos = GetChainBasePos(chainBase);
        PlanksTopPos = new Vector2(Chain.position.x, Chain.position.y);

        UpdatePosition();
	}

    public void DrawbridgeUp()
    { 
        Angle = Mathf.Clamp(Angle + 5, MinAngle, MaxAngle);
        UpdatePosition();
    }

    public void DrawbridgeDown()
    {
        Angle = Mathf.Clamp(Angle - 5, MinAngle, MaxAngle);
        UpdatePosition();
    }

    private void UpdateChainVisibility()
    {
        float chainLen = Vector3.Magnitude(GetLenOfChain());
        foreach (Transform item in Chain)
        {
            SpriteRenderer chainRenderer = item.GetComponent<SpriteRenderer>();
            if (chainLen > 0 && chainRenderer != null)
            {
                chainRenderer.enabled = true;
            }
            else
            {
                chainRenderer.enabled = false;
            }
            chainLen -= 2.25f;
        }
    }

    private void UpdatePosition()
    {
        Plank.localRotation = Quaternion.Euler(new Vector3(0, 0, Angle));
        PlanksTopPos = new Vector2(Chain.position.x, Chain.position.y);
        float chainAngle = GetAngleOfChain();
        Chain.localRotation = Quaternion.Euler(new Vector3(0, 0, chainAngle));
        UpdateChainVisibility();
    }

    private Vector2 GetChainBasePos(Transform chainBase)
    {
        Vector3 tmpLocal = chainBase.localPosition;
        tmpLocal.x -= 0.86f;
        chainBase.localPosition = tmpLocal;
        Vector2 basePos = new Vector2(chainBase.position.x , chainBase.position.y);
        tmpLocal.x += 0.86f;
        chainBase.localPosition = tmpLocal;
        return basePos;
    }

    private Vector2 GetLenOfChain()
    {
        return ChainBasePos - PlanksTopPos;
    }

    private float GetAngleOfChain()
    {
        Vector2 lenVec = GetLenOfChain();
        Vector2 chainBaseVec = PlanksTopPos - PlanksBasePos;
        float angle = Vector2.Angle(lenVec, chainBaseVec) - 180;
        return angle;
    }
}
