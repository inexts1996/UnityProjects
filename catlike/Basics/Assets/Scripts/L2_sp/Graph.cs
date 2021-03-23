﻿using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;

    [SerializeField] [Range(10, 100)] private int resolution = 10;

    [SerializeField] private FunctionLibrary.FunctionName function;
    // Start is called before the first frame update

    private Transform[] points;

    private void Awake()
    {
        var step = 2f / resolution;
        var scale = Vector3.one * step;
        points = new Transform[resolution * resolution];
        int count = points.Length;
        Transform point;

        for (int i = 0, x = 0, z = 0; i < count; ++i, ++x)
        {
            point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);

            points[i] = point;
        }
    }

    private void Start()
    {
    }

    private FunctionLibrary.Function func;
    // Update is called once per frame
    private void Update()
    {
        func = FunctionLibrary.GetFunctionWith(function);

        float time = Time.time;
        float step = 2f / resolution;
        float v =  0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; ++i, ++x)
        {
            if (x == resolution)
            {
                x = 0;
                ++z;
                v = (z + 0.5f) * step - 1f;
            }

            float u = (x + 0.5f) * step - 1f;
            points[i].localPosition = func.Invoke(u, v, time);
        }
    }
}