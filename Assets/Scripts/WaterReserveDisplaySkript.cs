using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WaterReserveDisplaySkript : MonoBehaviour 
{
    public float DistanceBetween = -35;

    public Transform ImagePrefab;
    private List<Pair<bool, Transform>> WaterReserveImages = new List<Pair<bool, Transform>>();

	// Use this for initialization
	void Start () 
    {

	}

    public void CreateImages(int limit)
    {
        var pos = ImagePrefab.position;

        while (WaterReserveImages.Count < limit)
        {
            var newImage = Instantiate(ImagePrefab, pos, ImagePrefab.rotation) as Transform;
            pos.y += DistanceBetween;
            newImage.SetParent(transform, false);
            //Debug.Log("image pos is: " + newImage.position.ToString());

            WaterReserveImages.Add(new Pair<bool, Transform>(false, newImage));
        }
    }

    public void DisableTopEnabled()
    {
        var query = from image in WaterReserveImages where image.First select image;
        if (query.Any())
        {
            Debug.Log("Any DisableTopEnabled");
            var result = query.First();
            result.First = false;
            var image = result.Second;

            var empty = image.FindChild("Empty");
            if (empty != null) empty.gameObject.SetActive(true);
            var full = image.FindChild("Full");
            if (full != null) full.gameObject.SetActive(false);
        }
    }

    public void EnableLowestDisabled()
    {
        var query = from image in WaterReserveImages where !image.First select image;
        if (query.Any())
        {
            Debug.Log("Any EnableLowestDisabled");
            var result = query.Last();
            result.First = true;
            var image = result.Second;

            var empty = image.FindChild("Empty");
            if (empty != null) empty.gameObject.SetActive(false);
            var full = image.FindChild("Full");
            if (full != null) full.gameObject.SetActive(true);
        }
    }

    public class Pair<F, S>
    {
        public Pair()
        {
        }

        public Pair(F first, S second)
        {
            this.First = first;
            this.Second = second;
        }

        public F First { get; set; }
        public S Second { get; set; }
    };
}
