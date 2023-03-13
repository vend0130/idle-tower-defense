using System;
using System.Collections.Generic;
using Code.Factories.Assets;
using Code.Game.Enemy;
using UnityEngine;

namespace Code.Factories.Enemies
{
    public class EnemiesFactory : IEnemiesFactory, IEnemiesPool
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly List<EnemyView> _enemys = new List<EnemyView>();

        public EnemiesFactory(IAssetsProvider assetsProvider) =>
            _assetsProvider = assetsProvider;

        public bool TryChangeTarget(Vector2 origin, float minimalDistance, out Transform target)
        {
            if (_enemys.Count == 0)
            {
                target = null;
                return false;
            }

            target = _enemys[0].EnemyTransform;
            float currentDistance = Vector2.Distance(origin, target.position);
            for (int i = 0; i < _enemys.Count; i++)
            {
                float newDistance = Vector2.Distance(origin, _enemys[i].EnemyTransform.position);
                if (newDistance < currentDistance)
                {
                    currentDistance = newDistance;
                    target = _enemys[i].EnemyTransform;
                }
            }

            if (currentDistance > minimalDistance)
            {
                target = null;
                return false;
            }

            return true;
        }

        public void CreateEnemy(EnemyType enemyType, Vector2 at, Transform heroTransform)
        {
            GameObject enemy = _assetsProvider.DiInstantiate(GetPath(enemyType), at);
            EnemyView enemyView = enemy.GetComponent<EnemyView>();
            enemyView.Init(heroTransform);
            _enemys.Add(enemyView);
        }

        private string GetPath(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Simply:
                    return AssetPath.SimplyEnemyPath;
                case EnemyType.Boss:
                    return AssetPath.BossEnemyPath;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null);
            }
        }
    }
}