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
        private const string NameParent = "Enemies";

        private readonly IAssetsProvider _assetsProvider;
        private readonly List<EnemyView> _enemiesOnScene = new List<EnemyView>();
        private readonly List<EnemyView> _boss = new List<EnemyView>();
        private readonly List<EnemyView> _simply = new List<EnemyView>();
        private readonly Vector2 _defaultPosition = new Vector2(100, 0);

        private GameObject _simplyEnemy;
        private GameObject _bossEnemy;
        private Transform _parent;

        public List<EnemyView> EmiesOnScene => _enemiesOnScene;

        public EnemiesFactory(IAssetsProvider assetsProvider) =>
            _assetsProvider = assetsProvider;

        public void Initialize() =>
            Warmup();

        private void Warmup()
        {
            _simplyEnemy = _assetsProvider.Warmup(AssetPath.SimplyEnemyPath);
            _bossEnemy = _assetsProvider.Warmup(AssetPath.BossEnemyPath);
            _parent = new GameObject(NameParent).transform;
        }

        public bool TryChangeTarget(Vector2 origin, float minimalDistance, out Transform target)
        {
            if (_enemiesOnScene.Count == 0)
            {
                target = null;
                return false;
            }

            target = _enemiesOnScene[0].EnemyTransform;
            float currentDistance = Vector2.Distance(origin, target.position);
            for (int i = 0; i < _enemiesOnScene.Count; i++)
            {
                float newDistance = Vector2.Distance(origin, _enemiesOnScene[i].EnemyTransform.position);
                if (newDistance < currentDistance)
                {
                    currentDistance = newDistance;
                    target = _enemiesOnScene[i].EnemyTransform;
                }
            }

            if (currentDistance > minimalDistance)
            {
                target = null;
                return false;
            }

            return true;
        }

        public void Spawn(EnemyType enemyType, Vector2 at, Transform heroTransform)
        {
            EnemyView enemyView;
            switch (enemyType)
            {
                case EnemyType.Boss when _boss.Count > 0:
                    enemyView = _boss[0];
                    _boss.Remove(enemyView);
                    break;
                case EnemyType.Simply when _simply.Count > 0:
                    enemyView = _simply[0];
                    _simply.Remove(enemyView);
                    break;
                default:
                    GameObject enemy = _assetsProvider.DiInstantiate(GetPrefab(enemyType), at);
                    enemyView = enemy.GetComponent<EnemyView>();
                    enemyView.transform.SetParent(_parent);
                    break;
            }

            enemyView.Spawn(heroTransform, at);
            _enemiesOnScene.Add(enemyView);
        }

        public void UnSpawn(EnemyView enemyView)
        {
            _enemiesOnScene.Remove(enemyView);

            switch (enemyView.EnemyType)
            {
                case EnemyType.Simply:
                    _simply.Add(enemyView);
                    break;
                case EnemyType.Boss:
                    _boss.Add(enemyView);
                    break;
            }

            enemyView.transform.position = _defaultPosition;
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