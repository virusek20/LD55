using UnityEngine;

public static class Ease
{
    public static float SineIn(float t)
    {
        return Mathf.Sin((t - 1) * Mathf.PI / 2) + 1;
    }

    public static float SineOut(float t)
    {
        return Mathf.Sin(t * Mathf.PI / 2);
    }

    public static float SineInOut(float t)
    {
        return (Mathf.Sin((t - 1) * Mathf.PI) + 1) / 2;
    }

    public static float PowIn(float t, float pow)
    {
        return Mathf.Pow(t, pow);
    }

    public static float PowOut(float t, float pow)
    {
        return 1 - Mathf.Pow(1 - t, pow);
    }

    public static float PowInOut(float t, float pow)
    {
        return t < 0.5 ? Mathf.Pow(t * 2, pow) / 2 : 1 - Mathf.Pow(1 - (t - 0.5f) * 2, pow) / 2;
    }

    public static float QuadIn(float t)
    {
        return PowIn(t, 2);
    }

    public static float QuadOut(float t)
    {
        return PowOut(t, 2);
    }

    public static float QuadInOut(float t)
    {
        return PowInOut(t, 2);
    }

    public static float CubicIn(float t)
    {
        return PowIn(t, 3);
    }

    public static float CubicOut(float t)
    {
        return PowOut(t, 3);
    }

    public static float CubicInOut(float t)
    {
        return PowInOut(t, 3);
    }

    public static float QuartIn(float t)
    {
        return PowIn(t, 4);
    }

    public static float QuartOut(float t)
    {
        return PowOut(t, 4);
    }

    public static float QuartInOut(float t)
    {
        return PowInOut(t, 4);
    }

    public static float QuintIn(float t)
    {
        return PowIn(t, 5);
    }

    public static float QuintOut(float t)
    {
        return PowOut(t, 5);
    }

    public static float QuintInOut(float t)
    {
        return PowInOut(t, 5);
    }

    public static float ExpoIn(float t)
    {
        return Mathf.Pow(2, 10 * (t - 1));
    }

    public static float ExpoOut(float t)
    {
        return -Mathf.Pow(2, -10 * t) + 1;
    }

    public static float ExpoInOut(float t)
    {
        return t < 0.5 ? Mathf.Pow(2, 20 * t - 10) / 2 : (-Mathf.Pow(2, -20 * t + 10) + 2) / 2;
    }

    public static float CircIn(float t)
    {
        return 1 - Mathf.Sqrt(1 - t * t);
    }

    public static float CircOut(float t)
    {
        return Mathf.Sqrt(1 - (--t) * t);
    }

    public static float CircInOut(float t)
    {
        return t < 0.5 ? (1 - Mathf.Sqrt(1 - 4 * t * t)) / 2 : (Mathf.Sqrt(1 - (-2 * t + 2) * (-2 * t + 2)) + 1) / 2;
    }

    public static float BackIn(float t, float amount = 1.5f)
    {
        return t * t * ((amount + 1f) * t - amount);
    }

    public static float BackOut(float t, float amount = 1.5f)
    {
        return (--t * t * ((amount + 1f) * t + amount) + 1f);
    }
    public static float BackInOut(float t, float amount = 1.5f)
    {
        t *= 2f;
        amount *= 1.525f;
        if (t < 1f)
        {
            return 0.5f * (t * t * ((amount + 1f) * t - amount));
        }
        else
        {
            t -= 2f;
            return 0.5f * (t * t * ((amount + 1f) * t + amount) + 2f);
        }
    }

    public static float ElasticIn(float t)
    {
        return Mathf.Sin(13 * Mathf.PI / 2 * t) * Mathf.Pow(2, 10 * (t - 1));
    }

    public static float ElasticOut(float t)
    {
        return Mathf.Sin(-13 * Mathf.PI / 2 * (t + 1)) * Mathf.Pow(2, -10 * t) + 1;
    }

    public static float ElasticInOut(float t)
    {
        return t < 0.5 ? Mathf.Sin(13 * Mathf.PI / 2 * (2 * t)) * Mathf.Pow(2, 10 * ((2 * t) - 1)) / 2 : (Mathf.Sin(-13 * Mathf.PI / 2 * ((2 * t - 1) + 1)) * Mathf.Pow(2, -10 * (2 * t - 1)) + 2) / 2;
    }

    public static float BounceIn(float t)
    {
        return Mathf.Pow(2, 6 * (t - 1)) * Mathf.Abs(Mathf.Sin(t * Mathf.PI * 3.5f));
    }

    public static float BounceOut(float t)
    {
        return 1 - Mathf.Pow(2, -6 * t) * Mathf.Abs(Mathf.Cos(t * Mathf.PI * 3.5f));
    }

    public static float BounceInOut(float t)
    {
        return t < 0.5 ? Mathf.Pow(2, 12 * (t - 1)) * Mathf.Abs(Mathf.Sin(t * Mathf.PI * 7)) / 2 : (1 - Mathf.Pow(2, -12 * (t - 1)) * Mathf.Abs(Mathf.Sin(t * Mathf.PI * 7))) / 2;
    }
}
