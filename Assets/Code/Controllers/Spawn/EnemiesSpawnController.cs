using System;
using System.Threading;
using Code.Factories.Enemies;
using Code.Model.Spawn;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Controllers.Spawn
{
    public class EnemiesSpawnController : ISpawnController, IDisposable
    {
        //TODO: to static data
        private const int SpawnDelay = 1000;
        private const float ChanceTopAndBottom = .65f;

        private const float HalfChance = .5f;

        private readonly IEnemiesFactory _enemiesFactory;
        private readonly ISpawnModel _spawnModel;
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        public EnemiesSpawnController(IEnemiesFactory enemiesFactory, ISpawnModel spawnModel)
        {
            _enemiesFactory = enemiesFactory;
            _spawnModel = spawnModel;
        }

        public void Dispose() =>
            DisposeToken();

        public void StartSpawn() =>
            Spawn();

        public void EndSpawn() =>
            DisposeToken();

        private async UniTask Delay()
        {
            await UniTask.Delay(SpawnDelay, cancellationToken: _cancellationToken.Token);
            Spawn();
        }

        private void Spawn()
        {
            _enemiesFactory.CreateEnemy(EnemyType.Simply, GetSpawnPoint());
            Delay().Forget();
        }

        private void DisposeToken()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
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
            if (Random.value < ChanceTopAndBottom)
                return Random.value > HalfChance ? Sides.Top : Sides.Bottom;

            return Random.value > HalfChance ? Sides.Left : Sides.Right;
        }
    }
}