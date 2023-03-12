using UnityEngine;
using Zenject;

namespace Code.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _diContainer;

        public GameFactory(DiContainer diContainer) =>
            _diContainer = diContainer;

        public void CreateHero()
        {
            var heroPrefab = Resources.Load(AssetPath.HeroPath) as GameObject;
            Instantiate(heroPrefab, Vector2.zero);
        }

        private GameObject Instantiate(GameObject prefab, Vector2 at) =>
            _diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, null);
    }
}