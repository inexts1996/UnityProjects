using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;

    [SerializeField] [Range(10, 100)] private int resolution = 10;

    [SerializeField] [Range(0, 1)] private int function;
    // Start is called before the first frame update

    private Transform[] points;

    private void Awake()
    {
        var step = 2f / resolution;
        var scale = Vector3.one * step;
        var position = Vector3.zero;
        points = new Transform[resolution];
        Transform point;

        for (var i = 0; i < resolution; ++i)
        {
            point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);

            points[i] = point;
        }
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        for (var i = 0; i < resolution; i++)
        {
            var point = points[i];
            var position = point.localPosition;
            if (function == 0)
                position.y = FunctionLibrary.Ware(position.x, Time.time);
            else
                position.y = FunctionLibrary.MultiWare(position.x, Time.time);
            point.localPosition = position;
        }
    }
}