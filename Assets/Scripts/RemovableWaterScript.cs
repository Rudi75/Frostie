using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RemovableWaterScript : MonoBehaviour 
{
    private IEnumerable<SpriteRenderer> waterRenders;
    private Collider2D waterCollider;

    void Start()
    {
        waterRenders = GetComponentsInChildren<SpriteRenderer>();
        waterCollider = GetComponent<Collider2D>();
    }

    public bool CanRemove()
    {
        if (waterRenders != null && waterCollider != null)
        {
            var query = from water in waterRenders where water.enabled select water;
            if (waterCollider.enabled == true && query.Count() == waterRenders.Count()) return true;
        }
        return false;
    }

    public bool CanPutBack()
    {
        if (waterRenders != null && waterCollider != null)
        {
            var query = from water in waterRenders where !water.enabled select water;
            if (waterCollider.enabled == false && query.Count() == waterRenders.Count()) return true;
        }
        return false;
    }

    public void Remove()
    {
        if(waterRenders != null)
            foreach (var waterRender in waterRenders)
            {
                waterRender.enabled = false;
            } 
        if (waterCollider != null) waterCollider.enabled = false;
    }

    public void PutBack()
    {
        if (waterRenders != null)
            foreach (var waterRender in waterRenders)
            {
                waterRender.enabled = true;
            } 
        if (waterCollider != null) waterCollider.enabled = true;
    }
}
