using System;
using UnityEngine;

public class GPUGraph : MonoBehaviour
{
    [SerializeField] [Range(10, 200)] private int resolution = 10;

    [SerializeField] private FunctionLibrary.FunctionName function;
    [SerializeField] private FunctionLibrary.TransitionMode transitionMode = FunctionLibrary.TransitionMode.Cycle;
    [SerializeField] [Min(0f)] private float functionDuration = 1f, transitionDuration = 1f;
    [SerializeField] private ComputeShader computeShader;

    private readonly int positionsId = Shader.PropertyToID("_Positions");
    private readonly int resolutionId = Shader.PropertyToID("_Resolution");
    private readonly int stepId = Shader.PropertyToID("_Step");
    private readonly int timeId = Shader.PropertyToID("_Time");

    private FunctionLibrary.Function func;

    private float duration;
    private ComputeBuffer positionBuffer;

    private void OnEnable()
    {
        positionBuffer = new ComputeBuffer(resolution * resolution, 3 * 4);
    }

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
        
        UpdateFunctionOnGPU();
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

    private bool transitioning;
    private FunctionLibrary.FunctionName transitionFunction;

    private void OnDisable()
    {
        positionBuffer.Release();
        positionBuffer = null;
    }

    private void UpdateFunctionOnGPU()
    {
        float step = 2f / resolution;
        computeShader.SetInt(resolutionId, resolution);
        computeShader.SetFloat(stepId, step);
        computeShader.SetFloat(timeId, Time.time);
        computeShader.SetBuffer(0, positionsId, positionBuffer);

        int groups = Mathf.CeilToInt(resolution / 8f);
        
        computeShader.Dispatch(0, groups, groups, 1);
    }
}