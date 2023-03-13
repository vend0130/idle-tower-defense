using System;
using System.Collections.Generic;
using Code.Game.Enemy;
using UnityEngine;

namespace Code.Factories.Enemies
{
    public interface IEnemiesPool
    {
        bool TryChangeTarget(Vector2 origin, out Transform target, float? minimalDistance = null);
        void UnSpawn(EnemyView enemyView);
        void AddControlEnemy();
        void RemoveControlEnemy(bool unSpawn = false);
        List<EnemyView> GetEnemiesInRadius(Vector2 origin, float radius, int count);
        event Action RemovedControlHandler;
    }
}