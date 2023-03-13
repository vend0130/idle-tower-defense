using System;
using System.Collections.Generic;
using Code.Factories.AssetsManagement;
using Code.Game.Enemy;
using UnityEngine;
using Zenject;

namespace Code.Factories.Enemies
{
    public class EnemiesFactory : IInitializable, IEnemiesFactory, IEnemiesPool
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly List<EnemyView> _enemys = new List<EnemyView>();

        private GameObject _simplyEnemy;
        private GameObject _bossEnemy;

        public EnemiesFactory(IAssetsProvider assetsProvider) =>
            _assetsProvider = assetsProvider;

        public void Initialize() => 
            Warmup();

        private void Warmup()
        {
            _simplyEnemy = _assetsProvider.Warmup(AssetPath.SimplyEnemyPath);
            _bossEnemy = _assetsProvider.Warmup(AssetPath.BossEnemyPath);
        }

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
            GameObject enemy = _assetsProvider.DiInstantiate(GetPrefab(enemyType), at);
            EnemyView enemyView = enemy.GetComponent<EnemyView>();
            enemyView.Init(heroTransform);
            _enemys.Add(enemyView);
        }

        private GameObject GetPrefab(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Simply:
                    return _simplyEnemy;
                case EnemyType.Boss:
                    return _bossEnemy;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null);
            }
        }
    }
}