using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
	[SerializeField]
	Transform pointPrefab = default;
    // Start is called before the first frame update
	
	void Awake()
	{
		Transform point;
		int i = 0;
		while(i < 10)
		{
			point = Instantiate(pointPrefab);
			point.localPosition = Vector3.right * i;
			++i;
		}
	}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
