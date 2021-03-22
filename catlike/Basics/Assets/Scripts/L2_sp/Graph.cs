using UnityEngine;

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
        var position = Vector3.zero;
        points = new Transform[resolution * resolution];
        int count = points.Length;
        Transform point;

        for (int i = 0, x = 0, z = 0; i < count; ++i, ++x)
        {
            if (x >= resolution)
            {
                x = 0;
                ++z;
            }
            point = Instantiate(pointPrefab);
            position.x = (x + 0.5f) * step - 1f;
            position.z = (z + 0.5f) * step - 1f;
            point.localPosition = position;
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
        for (var i = 0; i < resolution * resolution; i++)
        {
            var point = points[i];
            var position = point.localPosition;

            position.y = func.Invoke(position.x, position.z, Time.time);
            point.localPosition = position;
        }
    }
}