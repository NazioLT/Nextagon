using UnityEngine;

public struct Orientation
{
    public Orientation(float _f0, float _f1, float _f2, float _f3,
                        float _b0, float _b1, float _b2, float _b3, float _startAngle)
    {
        f0 = _f0;
        f1 = _f1;
        f2 = _f2;
        f3 = _f3;

        b0 = _b0;
        b1 = _b1;
        b2 = _b2;
        b3 = _b3;

        startAngle = _startAngle;
    }

    public readonly float f0, f1, f2, f3;
    public readonly float b0, b1, b2, b3;
    public readonly float startAngle;

    public static readonly Orientation LayoutPointy = new Orientation(Mathf.Sqrt(3f), Mathf.Sqrt(3f) / 2f, 0f, 3f / 2f,
                Mathf.Sqrt(3f) / 3f, -1f / 3f, 0f, 2f / 3f, 0.5f);

    public static readonly Orientation LayoutFlat = new Orientation(3f / 2f, 0f, Mathf.Sqrt(3f) / 2f, Mathf.Sqrt(3f),
                2f / 3f, 0f, -1f / 3f, Mathf.Sqrt(3f) / 3f, 0f);

    public override string ToString() => $"Orientation(f0 {f0}, f1 {f1}, f2 {f2}, f3 {f3}, b0 {b0}, b1 {b1}, b2 {b2}, b3 {b3})";
}