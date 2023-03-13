using Code.Controllers.Spawn;
using Code.Factories.Assets;
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

        public GameFactory(IAssetsProvider assetsProvider, DiContainer diContainer,
            ISpawnController spawnController, EndGameState endGameState)
        {
            _assetsProvider = assetsProvider;
            _diContainer = diContainer;
            _spawnController = spawnController;
            _endGameState = endGameState;
        }

        public HeroHealth CreateHero()
        {
            GameObject hero = _assetsProvider.DiInstantiate(AssetPath.HeroPath, Vector2.zero);
            _spawnController.InitHero(hero.transform);
            return hero.GetComponent<HeroHealth>();
        }

        public void CreateHud()
        {
            GameObject hud = _assetsProvider.DiInstantiate(AssetPath.HUDPath, Vector2.zero);
            _endGameState.InitHud(hud.GetComponent<HUD>());
            _diContainer.Bind<HPBar>().FromInstance(hud.GetComponentInChildren<HPBar>()).AsSingle();
        }

        public void CreateEndGame() =>
            _assetsProvider.DiInstantiate(AssetPath.EndGamePath, Vector2.zero);
    }
}