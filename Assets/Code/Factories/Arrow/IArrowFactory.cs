using UnityEngine;

namespace Code.Factories.Arrow
{
    public interface IArrowFactory
    {
        void Spawn(Vector2 point, Quaternion rotation, float speed);
    }
}