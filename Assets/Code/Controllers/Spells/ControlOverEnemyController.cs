using System;
using System.Threading;
using Code.Data;
using Code.Factories.Enemies;
using Code.Game.UI;
using Cysharp.Threading.Tasks;

namespace Code.Controllers.Spells
{
    public class ControlOverEnemyController : IControlOverEnemy, IDisposable
    {
        private readonly IEnemiesPool _enemiesPool;
        private readonly ControlOverEnemyData _data;

        private CancellationTokenSource _cancellationToken;

        private SpellView _spellView;

        public ControlOverEnemyController(IEnemiesPool enemiesPool, ControlOverEnemyData data)
        {
            _enemiesPool = enemiesPool;
            _data = data;

            _enemiesPool.RemovedControlHandler += DisposeToken;
        }

        public void Dispose()
        {
            _enemiesPool.RemovedControlHandler -= DisposeToken;
            DisposeToken();
        }


        public void InitSpellView(SpellView spellView)
        {
            _spellView = spellView;
            _spellView.ClickHandler += Cast;
        }

        private void Cast()
        {
            _cancellationToken = new CancellationTokenSource();
            _enemiesPool.AddControlEnemy();
            Delay().Forget();
        }

        private async UniTask Delay()
        {
            await UniTask.Delay(_data.TimeControlled, cancellationToken: _cancellationToken.Token);
            StopControlled();
        }

        private void StopControlled() =>
            _enemiesPool.RemoveControlEnemy();

        private void DisposeToken()
        {
            if (_cancellationToken == null)
                return;

            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
            _cancellationToken = null;
        }
    }

    public interface IControlOverEnemy
    {
        void InitSpellView(SpellView spellView);
    }
}