using Code.Factories.Assets;
using UnityEngine;

namespace Code.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetsProvider;

        public GameFactory(IAssetsProvider assetsProvider) =>
            _assetsProvider = assetsProvider;

        public void CreateHero() =>
            _assetsProvider.DiInstantiate(AssetPath.HeroPath, Vector2.zero);
    }
}