using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCube : MonoBehaviour
{
	public float _rotateSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	transform.RotateAround(Vector3.zero, Vector3.up, _rotateSpeed * Time.deltaTime);
    }
}
