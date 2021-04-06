using UnityEngine;

public class GPUGraph : MonoBehaviour
{
    [SerializeField] [Range(10, 200)] private int resolution = 10;

    [SerializeField] private FunctionLibrary.FunctionName function;
    [SerializeField] private FunctionLibrary.TransitionMode transitionMode = FunctionLibrary.TransitionMode.Cycle;
    [SerializeField] [Min(0f)] private float functionDuration = 1f, transitionDuration = 1f;


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
}