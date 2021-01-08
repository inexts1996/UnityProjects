using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab = default;

    private void Awake()
    {
        Transform point;
        for (int i = 0; i < 10; ++i)
        {
            point = Instantiate(pointPrefab);
            point.localPosition = Vector3.right * (i / 5f - 1f);
            point.localScale = Vector3.one / 5f;
        }
    }
}