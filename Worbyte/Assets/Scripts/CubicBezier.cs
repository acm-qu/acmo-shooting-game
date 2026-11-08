using UnityEngine;

public static class CubicBezier
{
    // Same as CSS cubic-bezier(x1, y1, x2, y2). time: 0..1, returns progress.
    public static float Evaluate(float x1, float y1, float x2, float y2, float time)
    {
        time = Mathf.Clamp01(time);
        float lo = 0f, hi = 1f, u = time;
        for (int i = 0; i < 20; i++)          // find the curve point where x == time
        {
            u = (lo + hi) * 0.5f;
            if (Bez(x1, x2, u) < time) lo = u; else hi = u;
        }
        return Bez(y1, y2, u);
    }

    private static float Bez(float a, float b, float u)
    {
        float inv = 1f - u;
        return 3f * inv * inv * u * a + 3f * inv * u * u * b + u * u * u;
    }
}