using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class PathDefinition : MonoBehaviour
{
    public List<Transform> Points = new List<Transform>();
	//public Transform[] Points;
	
	public IEnumerator<Transform> GetPathEnumerator()
	{
		if(Points == null || Points.Count < 1)
			yield break;
		
		var direction = 1;
		var index = 0;
		while (true)
		{
			yield return Points[index];

            if (Points.Count == 1)
				continue;
			
			if (index <= 0)
				direction = 1;
            else if (index >= Points.Count - 1)
				direction = -1;
			
			index = index + direction;
		}
	}

    public Transform GetFirstOrDefault()
    {
        return Points.FirstOrDefault();
    }

    public Transform GetLastOrDefault()
    {
        return Points.LastOrDefault();
    }
	
	public void OnDrawGizmos()
	{
        if (Points == null || Points.Count < 2)
			return;

        var points = Points.Where(t => t != null).ToList();
        if(points.Count < 2)
            return;
		
		
		for (var i = 1; i < points.Count; i++) {
			Gizmos.DrawLine (points [i - 1].position, Points [i].position);
		}
	}
}