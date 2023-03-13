using UnityEngine;

namespace Code.Game
{
    public interface IHealth
    {
        UnitType Unit { get; }
        Transform Current { get; }
        void TakeDamage(float damage);
    }
}