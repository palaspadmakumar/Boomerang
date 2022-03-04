using com.hellion.shapes.interfaces;
using UnityEngine;

public class BoomerangEllipseShape : IShape
{
    private float radius_1 = 0;
    private float radius_2 = 0;

    public BoomerangEllipseShape(float radius_1, float radius_2)
    {
        this.radius_1 = radius_1;
        this.radius_2 = radius_2;
    }

    public Vector3 GetPoint(float t)
    {
        float angle = t * Mathf.PI * 2;
        float modifiedTime = 1 - Mathf.Abs(t - 0.5f) * 2;
        float x = -Mathf.Sin(angle) * ((1 - modifiedTime) * (radius_1 * 0.3f) + modifiedTime * radius_1);
        float y = -Mathf.Cos(angle) * radius_2;
        return new Vector3(x, y);
    }
}
