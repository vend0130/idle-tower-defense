using System;
using System.Threading;
using Code.Data;
using Code.Factories.Enemies;
using Code.Model.Spawn;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Controllers.Spawn
{
    public class EnemiesSpawnController : ISpawnController, IDisposable
    {
        private const float HalfChance = .5f;

        private readonly IEnemiesFactory _enemiesFactory;
        private readonly ISpawnModel _spawnModel;
        private readonly GameData _gameData;

        private CancellationTokenSource _cancellationToken;
        private Transform _heroTransform;
        private float _nextTimeForSpawnBoss;

        public EnemiesSpawnController(IEnemiesFactory enemiesFactory, ISpawnModel spawnModel, GameData gameData)
        {
            _enemiesFactory = enemiesFactory;
            _spawnModel = spawnModel;
            _gameData = gameData;
        }

        public void InitHero(Transform heroTransform) =>
            _heroTransform = heroTransform;

        public void Dispose() =>
            DisposeToken();

        public void StartSpawn()
        {
            _nextTimeForSpawnBoss = Time.time + _gameData.SpawnTimeBossEnemy;
            _cancellationToken = new CancellationTokenSource();
            Spawn();
        }

        public void EndSpawn() =>
            DisposeToken();

        private async UniTask Delay()
        {
            await UniTask.Delay(_gameData.SpawnTimeSimplyEnemy, cancellationToken: _cancellationToken.Token);
            Spawn();
        }

        private void Spawn()
        {
            if (_heroTransform == null)
                return;

            EnemyType enemyType = EnemyType.Simply;

            if (Time.time > _nextTimeForSpawnBoss)
            {
                enemyType = EnemyType.Boss;
                _nextTimeForSpawnBoss = Time.time + _gameData.SpawnTimeBossEnemy;
            }

            _enemiesFactory.Spawn(enemyType, GetSpawnPoint(), _heroTransform);
            Delay().Forget();
        }

        private void DisposeToken()
        {
            if (_cancellationToken == null)
                return;

            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
            _cancellationToken = null;
        }

        private Vector2 GetSpawnPoint()
        {
            Sides side = GetSide();
            SidePoints points = _spawnModel.GetSide(side);
            switch (side)
            {
                case Sides.Top:
                case Sides.Bottom:
                    return new Vector2(Random.Range(points.FirstPoint, points.SecondPoint), points.StaticPoint);
                case Sides.Left:
                case Sides.Right:
                    return new Vector2(points.StaticPoint, Random.Range(points.FirstPoint, points.SecondPoint));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Sides GetSide()
        {
            if (Random.value < _gameData.ChanceSpawnInBottomAndTop)
                return Random.value > HalfChance ? Sides.Top : Sides.Bottom;

            return Random.value > HalfChance ? Sides.Left : Sides.Right;
        }
    }
}