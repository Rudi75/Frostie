using UnityEngine;
using System.Collections;

public class RemovableWaterScript : MonoBehaviour 
{
    private SpriteRenderer waterRender;
    private Collider2D waterCollider;

    void Start()
    {
        waterRender = GetComponent<SpriteRenderer>();
        waterCollider = GetComponent<Collider2D>();
    }

    public bool CanRemove()
    {
        if (waterRender != null && waterCollider != null)
            if(waterRender.enabled == true && waterRender.enabled == true) return true;
        return false;
    }

    public bool CanPutBack()
    {
        if (waterRender != null && waterCollider != null)
            if (waterRender.enabled == false && waterRender.enabled == false) return true;
        return false;
    }

    public void Remove()
    {
        if(waterRender != null) waterRender.enabled = false;
        if (waterCollider != null) waterRender.enabled = false;
    }

    public void PutBack()
    {
        if (waterRender != null) waterRender.enabled = true;
        if (waterCollider != null) waterRender.enabled = true;
    }
}
