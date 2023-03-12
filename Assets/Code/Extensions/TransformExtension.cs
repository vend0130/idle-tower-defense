using UnityEngine;

namespace Code.Extensions
{
    public static class TransformExtension
    {
        public static void LookAt2D(this Transform current, Transform target)
        {
            Vector2 direction = target.position - current.position;
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            current.eulerAngles = new Vector3(0, 0, rotation - 90);
        }
    }
}