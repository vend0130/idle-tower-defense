using System;
using System.Threading;
using Code.Factories.Enemies;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Controllers
{
    public class EnemiesSpawnController : ISpawnController, IDisposable
    {
        private readonly IEnemiesFactory _enemiesFactory;
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        public EnemiesSpawnController(IEnemiesFactory enemiesFactory)
        {
            _enemiesFactory = enemiesFactory;
        }

        public void Dispose() =>
            DisposeToken();

        public void StartSpawn() =>
            Spawn();

        public void EndSpawn() =>
            DisposeToken();

        private async UniTask Delay()
        {
            await UniTask.Delay(5000, cancellationToken: _cancellationToken.Token);
            Spawn();
        }

        private void Spawn()
        {
            _enemiesFactory.CreateEnemy(EnemyType.Simply, new Vector2(0, 5));
            Delay().Forget();
        }

        private void DisposeToken()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
        }
    }
}