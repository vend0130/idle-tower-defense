using System.Threading;
using Code.Data;
using Code.Factories.Enemies;
using Code.Game.UI;
using Cysharp.Threading.Tasks;

namespace Code.Controllers.Spells
{
    public class ControlOverEnemyController : IControlOverEnemy
    {
        private readonly IEnemiesPool _enemiesPool;
        private readonly ControlOverEnemyData _data;
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        private SpellView _spellView;

        public ControlOverEnemyController(IEnemiesPool enemiesPool, ControlOverEnemyData data)
        {
            _enemiesPool = enemiesPool;
            _data = data;
        }

        public void InitSpellView(SpellView spellView)
        {
            _spellView = spellView;
            _spellView.ClickHandler += Cast;
        }

        private void Cast()
        {
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
    }

    public interface IControlOverEnemy
    {
        void InitSpellView(SpellView spellView);
    }
}