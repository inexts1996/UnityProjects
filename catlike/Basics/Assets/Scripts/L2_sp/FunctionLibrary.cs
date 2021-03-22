using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public delegate Vector3 Function(float u, float v, float t);

    public enum FunctionName
    {
        Ware,
        MultiWare,
        Ripple
    }

    private static readonly Function[] functions =
    {
        Ware,
        MultiWare1,
        Ripple
    };

    public static Function GetFunctionWith(FunctionName name)
    {
        return functions[(int)name];
    }

    public static float Ware(float x, float z, float t)
    {
        return Sin(PI * (x + z + t));
    }

    public static float MultiWare(float x, float z, float t)
    {
        var y = Sin(PI * (x + 0.5f + t));
        y += 0.5f * Sin(2f * PI * (z + t));
        return y * (2f / 3f);
    }

    public static float MultiWare1(float x, float z, float t)
    {
        var y = Sin(PI * (x + 0.5f + t));
        y += 0.5f * Sin(2f * PI * (z + t));
        y += Sin(PI * (x + z + 0.25f * t));
        return y * (1f / 2.5f);
    }
    public static float Ripple(float x, float z, float t)
    {
        var d = Sqrt(x * x + z * z);

        var y = Sin(PI * (4f * d - t));
        return y / (1f + 10f * d);
    }
}