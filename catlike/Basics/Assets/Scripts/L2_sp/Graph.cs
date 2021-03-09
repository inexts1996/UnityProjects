using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
	[SerializeField]
	Transform pointPrefab = default;
	[SerializeField, Range(10, 100)]
	int resolution = 10;
    // Start is called before the first frame update
	
		Transform[] points;
	void Awake()
	{
		float step = 2f / resolution;
		var scale = Vector3.one * step;
		var position = Vector3.zero;
		points = new Transform[resolution];
		Transform point;

		for(int i = 0; i < resolution; ++i)
		{
			point = Instantiate(pointPrefab);
			position.x=  (i + 0.5f) * step - 1f;
			point.localPosition = position; 
			point.localScale =scale;
			point.SetParent(transform, false);

			points[i] = point;
		}
	}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    for (int i = 0; i <resolution; i++)
	    {
		    Transform point = points[i];
		    Vector3 position = point.localPosition;
		    position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
		    point.localPosition = position;
	    } 
    }
}
