using com.hellion.shapes.interfaces;
using UnityEngine;

namespace com.hellion.shapes.utils
{
    public class GenericShape : IShape
    {
        private float radius = 0;
        public GenericShape(float radius)
        {
            this.radius = radius;
        }
        public Vector3 GetPoint(float t)
        {
            float angle = t * Mathf.PI * 2;
            float x = -Mathf.Sin(angle) * radius;
            float y = -Mathf.Cos(angle) * radius;
            return new Vector3(x, y);
        }
    }
}
