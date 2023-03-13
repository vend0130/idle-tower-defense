using System.Collections.Generic;
using Code.Game.Enemy;
using UnityEngine;

namespace Code.Factories.Enemies
{
    public interface IEnemiesFactory
    {
        void Spawn(EnemyType enemyType, Vector2 at, Transform heroTransform);
    }
}