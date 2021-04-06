using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;

    [SerializeField] [Range(10, 200)] private int resolution = 10;

    [SerializeField] private FunctionLibrary.FunctionName function;
    [SerializeField] private FunctionLibrary.TransitionMode transitionMode = FunctionLibrary.TransitionMode.Cycle;
    [SerializeField] [Min(0f)] private float functionDuration = 1f, transitionDuration = 1f;


    private Transform[] points;

    private void Awake()
    {
        var step = 2f / resolution;
        var scale = Vector3.one * step;
        points = new Transform[resolution * resolution];
        var count = points.Length;
        Transform point;

        for (int i = 0, x = 0, z = 0; i < count; ++i, ++x)
        {
            point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);

            points[i] = point;
        }
    }

    private FunctionLibrary.Function func;

    private float duration;

    // Update is called once per frame
    private void Update()
    {
        duration += Time.deltaTime;

        if (transitioning)
        {
            if (duration >= transitionDuration)
            {
                duration -= transitionDuration;
                transitioning = false;
            }
        }
        else if (duration >= functionDuration)
        {
            duration -= functionDuration;
            transitioning = true;
            transitionFunction = function;
            PickFunction();
        }

        if (transitioning)
            UpdateFunctionTransition();
        else
            UpdateFunction();
    }

    private void PickFunction()
    {
        if (transitionMode == FunctionLibrary.TransitionMode.Cycle)
        {
            function = FunctionLibrary.GetNextFunctionName(function);
            return;
        }

        function = FunctionLibrary.GetRandomFunctionNameOtherThan(function);
    }

    private void UpdateFunction()
    {
        func = FunctionLibrary.GetFunctionWith(function);

        var time = Time.time;
        var step = 2f / resolution;
        var v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; ++i, ++x)
        {
            if (x == resolution)
            {
                x = 0;
                ++z;
                v = (z + 0.5f) * step - 1f;
            }

            var u = (x + 0.5f) * step - 1f;
            points[i].localPosition = func.Invoke(u, v, time);
        }
    }

    private bool transitioning;
    private FunctionLibrary.FunctionName transitionFunction;

    private void UpdateFunctionTransition()
    {
        FunctionLibrary.Function from = FunctionLibrary.GetFunctionWith(transitionFunction),
            to = FunctionLibrary.GetFunctionWith(function);

        var progress = duration / transitionDuration;
        var time = Time.time;
        var step = 2f / resolution;
        var v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; ++i, ++x)
        {
            if (x == resolution)
            {
                x = 0;
                ++z;
                v = (z + 0.5f) * step - 1f;
            }

            var u = (x + 0.5f) * step - 1f;
            points[i].localPosition = FunctionLibrary.Morph(u, v, time, from, to, progress);
        }
    }
}