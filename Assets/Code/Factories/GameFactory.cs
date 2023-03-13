using System;
using Code.Controllers.Spawn;
using Code.Controllers.Spells;
using Code.Data;
using Code.Factories.AssetsManagement;
using Code.Game.Hero;
using Code.Game.UI;
using Code.infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Code.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly DiContainer _diContainer;
        private readonly ISpawnController _spawnController;
        private readonly EndGameState _endGameState;
        private readonly GameData _gameData;
        private readonly MeteoriteData _meteoriteData;
        private readonly ControlOverEnemyData _controlOverEnemyData;
        private readonly LightningData _lightningData;

        private HUD _hud;

        public GameFactory(IAssetsProvider assetsProvider, DiContainer diContainer,
            ISpawnController spawnController, EndGameState endGameState, GameData gameData,
            MeteoriteData meteoriteData, ControlOverEnemyData controlOverEnemyData,
            LightningData lightningData)
        {
            _assetsProvider = assetsProvider;
            _diContainer = diContainer;
            _spawnController = spawnController;
            _endGameState = endGameState;
            _gameData = gameData;
            _meteoriteData = meteoriteData;
            _controlOverEnemyData = controlOverEnemyData;
            _lightningData = lightningData;
        }

        public void CreateHud()
        {
            GameObject hud = Instantiate(AssetPath.HUDPath, Vector2.zero);
            _diContainer.Bind<HPBar>().FromInstance(hud.GetComponentInChildren<HPBar>()).AsSingle();

            _hud = hud.GetComponent<HUD>();
            _endGameState.InitHud(_hud);
        }

        public HeroHealth CreateHero()
        {
            GameObject hero = Instantiate(AssetPath.HeroPath, _gameData.HeroSpawnPoint);

            var health = hero.GetComponent<HeroHealth>();
            _diContainer.Bind<IVampire>().FromInstance(health).AsSingle();

            _spawnController.InitHero(hero.transform);

            return health;
        }

        public void CreateEndGame() =>
            Instantiate(AssetPath.EndGamePath, Vector2.zero);

        public SpellView CreateSpell(SpellType spellType)
        {
            GameObject spell = Instantiate(AssetPath.SpellViewPath, Vector2.zero);
            spell.transform.SetParent(_hud.SpellsParent);
            spell.transform.localScale = Vector3.one;

            SpellView spellView = spell.GetComponent<SpellView>();

            switch (spellType)
            {
                case SpellType.Meteorite:
                    spellView.SetData(_meteoriteData.Cooldown, _meteoriteData.Text);
                    break;
                case SpellType.ControlOverEnemy:
                    spellView.SetData(_controlOverEnemyData.Cooldown, _controlOverEnemyData.Text);
                    break;
                case SpellType.Lightning:
                    spellView.SetData(_lightningData.Cooldown, _lightningData.Text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spellType), spellType, null);
            }

            return spell.GetComponent<SpellView>();
        }

        private GameObject Instantiate(string path, Vector2 at)
        {
            GameObject prefab = _assetsProvider.Warmup(path);
            return _assetsProvider.DiInstantiate(prefab, at);
        }
    }
}