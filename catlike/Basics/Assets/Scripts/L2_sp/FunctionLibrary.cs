using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public delegate Vector3 Function(float u, float v, float t);

    public enum FunctionName
    {
        Ware,
        MultiWare,
        Ripple,
        Sphere
    }

    private static readonly Function[] functions =
    {
        Ware,
        MultiWare1,
        Ripple,
        Sphere
    };

    public static Function GetFunctionWith(FunctionName name)
    {
        return functions[(int) name];
    }

    public static Vector3 Ware(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;
        return p;
    }

    public static Vector3 MultiWare(float u, float v, float t)
    {
        Vector3 p;
        var y = Sin(PI * (u + 0.5f + t));
        y += 0.5f * Sin(2f * PI * (v + t));

        p.x = u;
        p.y = y * (2f / 3f);
        p.z = v;
        return p;
    }

    public static Vector3 MultiWare1(float u, float v, float t)
    {
        var y = Sin(PI * (u + 0.5f + t));
        y += 0.5f * Sin(2f * PI * (v + t));
        y += Sin(PI * (u + v + 0.25f * t));

        Vector3 p;
        p.x = u;
        p.y = y * (1f / 2.5f);
        p.z = v;
        return p;
    }

    public static Vector3 Ripple(float u, float v, float t)
    {
        var d = Sqrt(u * u + v * v);
        var y = Sin(PI * (4f * d - t));

        Vector3 p;
        p.x = u;
        p.y = y / (1f + 10f * d);
        p.z = v;
        return p;
    }

    public static Vector3 Sphere(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.9f + 0.1f * Sin( PI * (6f * u + 4f * v + t));
        float s = r * Cos(0.5f * PI * v);
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * 0.5f * v);
        p.z = s * Cos(PI * u);

        return p;
    }
}