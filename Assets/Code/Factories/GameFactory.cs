using Code.Factories.Assets;
using Code.Game.Hero;
using UnityEngine;
using Zenject;

namespace Code.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly DiContainer _diContainer;

        public GameFactory(IAssetsProvider assetsProvider, DiContainer diContainer)
        {
            _assetsProvider = assetsProvider;
            _diContainer = diContainer;
        }

        public void CreateHero()
        {
            var hero =_assetsProvider.DiInstantiate(AssetPath.HeroPath, Vector2.zero);
            _diContainer.Bind<HeroRotation>().FromInstance(hero.GetComponent<HeroRotation>());
        }
    }
}