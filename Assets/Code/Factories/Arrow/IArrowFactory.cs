using UnityEngine;

namespace Code.Factories.Arrow
{
    public interface IArrowFactory
    {
        void Spawn(Transform attacker, float damage, Vector2 point, Quaternion rotation, float speed);
    }
}