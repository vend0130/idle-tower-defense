using UnityEngine;

namespace Code.Extensions
{
    public static class TransformExtension
    {
        public static void LookAt2D(this Transform current, Vector2 targetPoint)
        {
            Vector2 direction = targetPoint - (Vector2)current.position;
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            current.eulerAngles = new Vector3(0, 0, rotation - 90);
        }
    }
}