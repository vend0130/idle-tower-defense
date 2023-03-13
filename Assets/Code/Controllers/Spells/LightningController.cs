using Code.Data;
using Code.Factories.Enemies;
using Code.Game.UI;
using Code.Views.Spells;

namespace Code.Controllers.Spells
{
    public class LightningController : ILightningController
    {
        private readonly GameData _gameData;
        private readonly LightningData _data;
        private readonly IEnemiesPool _enemiesPool;
        private readonly LightningView _lightningView;

        private SpellView _spellView;

        public LightningController(GameData gameData, LightningData data, IEnemiesPool enemiesPool,
            LightningView lightningView)
        {
            _gameData = gameData;
            _data = data;
            _enemiesPool = enemiesPool;
            _lightningView = lightningView;
        }

        public void InitSpellView(SpellView spellView)
        {
            _spellView = spellView;
            _spellView.ClickHandler += Cast;
        }

        private void Cast()
        {
            var enemies =
                _enemiesPool.GetEnemiesInRadius(_gameData.HeroSpawnPoint, _data.Radius, _data.CountSteps);

            _lightningView.CastLightning(_gameData.HeroSpawnPoint, enemies, _data.DelayToHide, _data.Damage);
        }
    }

    public interface ILightningController
    {
        void InitSpellView(SpellView spellView);
    }
}