using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public delegate float Function(float x, float z, float t);

    public enum FunctionName
    {
        Ware,
        MultiWare,
        Ripple
    }

    private static readonly Function[] functions =
    {
        Ware,
        MultiWare,
        Ripple
    };

    public static Function GetFunctionWith(FunctionName name)
    {
        return functions[(int)name];
    }

    public static float Ware(float x, float z, float t)
    {
        return Sin(PI * (x + t));
    }

    public static float MultiWare(float x, float z, float t)
    {
        var y = Sin(PI * (x + 0.5f + t));
        y += 0.5f * Sin(2f * PI * (x + t));
        return y * (2f / 3f);
    }

    public static float Ripple(float x, float z, float t)
    {
        var d = Abs(x);

        var y = Sin(PI * (4f * d - t));
        return y / (1f + 10f * d);
    }
}