﻿using System;
using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Factories.AssetsManagement;
using Code.Game.Enemy;
using UnityEngine;
using Zenject;

namespace Code.Factories.Enemies
{
    public class EnemiesFactory : IInitializable, IEnemiesFactory, IEnemiesPool
    {
        public event Action RemovedControlHandler;

        private const string NameParent = "Enemies";

        private readonly IAssetsProvider _assetsProvider;
        private readonly List<EnemyView> _enemiesOnScene = new List<EnemyView>();
        private readonly List<EnemyView> _boss = new List<EnemyView>();
        private readonly List<EnemyView> _simply = new List<EnemyView>();
        private readonly Vector2 _defaultPosition = new Vector2(100, 0);
        private EnemyView _controlledEnemie;

        private GameObject _simplyEnemy;
        private GameObject _bossEnemy;
        private Transform _parent;

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

        public void AddControlEnemy()
        {
            if (_controlledEnemie != null)
                return;

            EnemyView randomEnemy = _enemiesOnScene.Where(x => x.EnemyType != EnemyType.Boss).GetRandom();
            _enemiesOnScene.Remove(randomEnemy);
            _controlledEnemie = randomEnemy;

            _controlledEnemie.ControlledView.ChangeControlledState(true);
            _enemiesOnScene.ForEach((currentEnemy) => currentEnemy.AddTarget(_controlledEnemie.EnemyTransform));
        }

        public void RemoveControlEnemy(bool unSpawn = false)
        {
            if (_controlledEnemie == null)
                return;

            RemovedControlHandler?.Invoke();

            if (!unSpawn)
            {
                AddEnemy(_controlledEnemie);
                _controlledEnemie.ControlledView.ChangeControlledState(false);
            }

            _enemiesOnScene.ForEach((currentEnemy) => currentEnemy.RemoveTarget(_controlledEnemie.EnemyTransform));
            _controlledEnemie = null;
        }

        public bool TryChangeTarget(Vector2 origin, out Transform target, float? minimalDistance = null)
        {
            if (_enemiesOnScene.Count == 0)
            {
                target = null;
                return false;
            }

            target = _enemiesOnScene[0].EnemyTransform;
            float currentDistance = Vector2.Distance(origin, target.position);
            for (int i = 1; i < _enemiesOnScene.Count; i++)
            {
                float newDistance = Vector2.Distance(origin, _enemiesOnScene[i].EnemyTransform.position);
                if (newDistance < currentDistance)
                {
                    currentDistance = newDistance;
                    target = _enemiesOnScene[i].EnemyTransform;
                }
            }

            if (minimalDistance != null && currentDistance > minimalDistance)
            {
                target = null;
                return false;
            }

            return true;
        }

        public List<EnemyView> GetEnemiesInRadius(Vector2 origin, float radius, int count)
        {
            List<EnemyView> enemyViews = new List<EnemyView>();
            for (int i = 0; i < _enemiesOnScene.Count; i++)
            {
                float distance = Vector2.Distance(origin, _enemiesOnScene[i].EnemyTransform.position);

                if (distance < radius / 2)
                    enemyViews.Add(_enemiesOnScene[i]);

                if (enemyViews.Count == count)
                    return enemyViews;
            }

            return enemyViews;
        }

        public void Spawn(EnemyType enemyType, Vector2 at, Transform heroTransform)
        {
            EnemyView enemyView;
            switch (enemyType)
            {
                case EnemyType.Boss when _boss.Count > 0:
                    enemyView = GetEnemy(_boss);
                    break;
                case EnemyType.Simply when _simply.Count > 0:
                    enemyView = GetEnemy(_simply);
                    break;
                default:
                    GameObject enemy = _assetsProvider.DiInstantiate(GetPrefab(enemyType), at);
                    enemyView = enemy.GetComponent<EnemyView>();
                    enemyView.transform.SetParent(_parent);
                    break;
            }

            enemyView.Spawn(heroTransform, at, this);

            if (_controlledEnemie != null)
                enemyView.AddTarget(_controlledEnemie.EnemyTransform);

            AddEnemy(enemyView);
        }

        public void UnSpawn(EnemyView enemyView)
        {
            switch (enemyView.EnemyType)
            {
                case EnemyType.Simply:
                    _simply.Add(enemyView);
                    break;
                case EnemyType.Boss:
                    _boss.Add(enemyView);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_controlledEnemie != null && enemyView == _controlledEnemie)
                RemoveControlEnemy(unSpawn: true);
            else
                _enemiesOnScene.Remove(enemyView);

            enemyView.EnemyTransform.position = _defaultPosition;
        }

        private EnemyView GetEnemy(List<EnemyView> enemies)
        {
            EnemyView enemy = enemies[0];
            enemies.Remove(enemy);

            return enemy;
        }

        private void AddEnemy(EnemyView enemyView)
        {
            if (_enemiesOnScene.Contains(enemyView))
                return;

            _enemiesOnScene.Add(enemyView);
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