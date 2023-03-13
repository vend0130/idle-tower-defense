using Code.Controllers.Spawn;
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

        public GameFactory(IAssetsProvider assetsProvider, DiContainer diContainer,
            ISpawnController spawnController, EndGameState endGameState, GameData gameData)
        {
            _assetsProvider = assetsProvider;
            _diContainer = diContainer;
            _spawnController = spawnController;
            _endGameState = endGameState;
            _gameData = gameData;
        }

        public void CreateHud()
        {
            GameObject hud = Instantiate(AssetPath.HUDPath, Vector2.zero);

            _endGameState.InitHud(hud.GetComponent<HUD>());
            _diContainer.Bind<HPBar>().FromInstance(hud.GetComponentInChildren<HPBar>()).AsSingle();
        }

        public HeroHealth CreateHero()
        {
            GameObject hero = Instantiate(AssetPath.HeroPath, _gameData.HeroSpawnPoint);

            _spawnController.InitHero(hero.transform);
            return hero.GetComponent<HeroHealth>();
        }

        public void CreateEndGame() =>
            Instantiate(AssetPath.EndGamePath, Vector2.zero);

        private GameObject Instantiate(string path, Vector2 at)
        {
            GameObject prefab = _assetsProvider.Warmup(path);
            return _assetsProvider.DiInstantiate(prefab, at);
        }
    }
}