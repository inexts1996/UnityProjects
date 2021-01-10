using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;
    [SerializeField, Range(10, 100)] private int resolution = 20;
    private GameObject _root;

    private void Awake()
    {
        _root = new GameObject("root");
        float step = 2f / resolution;
        Vector3 position = Vector3.zero * step;
        Transform point;
        Vector3 scale;
        scale = Vector3.one * step;
        for (var i = 0; i < resolution; ++i)
        {
            point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            position.y = position.x * position.x;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(_root.transform, false);
        }
    }
}