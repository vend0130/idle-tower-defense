using UnityEngine;
using Zenject;

namespace Code.Factories.Assets
{
    public class AssetsProvider : IAssetsProvider
    {
        private readonly DiContainer _diContainer;

        public AssetsProvider(DiContainer diContainer) =>
            _diContainer = diContainer;

        public GameObject DiInstantiate(string path, Vector2 at)
        {
            var prefab = Resources.Load(path) as GameObject;
            return _diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, null);
        }

        public GameObject Instantiate(string path, Vector2 at)
        {
            var prefab = Resources.Load(path) as GameObject;
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}