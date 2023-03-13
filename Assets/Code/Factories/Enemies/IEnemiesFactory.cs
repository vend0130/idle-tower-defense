using UnityEngine;

namespace Code.Factories.Enemies
{
    public interface IEnemiesFactory
    {
        void CreateEnemy(EnemyType enemyType, Vector2 at, Transform heroTransform);
    }
}