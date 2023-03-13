using UnityEngine;

namespace Code.Game
{
    public interface IHealth
    {
        Transform Current { get; }
        void TakeDamage(float damage);
    }
}