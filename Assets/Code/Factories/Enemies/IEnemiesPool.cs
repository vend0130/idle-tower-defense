using Code.Game.Enemy;
using UnityEngine;

namespace Code.Factories.Enemies
{
    public interface IEnemiesPool
    {
        bool TryChangeTarget(Vector2 origin, float minimalDistance, out Transform target);
        void UnSpawn(EnemyView enemyView);
    }
}