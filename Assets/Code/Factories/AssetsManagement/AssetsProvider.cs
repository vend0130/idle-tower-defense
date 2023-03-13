using UnityEngine;
using Zenject;

namespace Code.Factories.AssetsManagement
{
    public class AssetsProvider : IAssetsProvider
    {
        private readonly DiContainer _diContainer;

        public AssetsProvider(DiContainer diContainer) =>
            _diContainer = diContainer;

        public GameObject Warmup(string path) => 
            Resources.Load(path) as GameObject;

        public GameObject DiInstantiate(GameObject prefab, Vector2 at)
        {
            return _diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, null);
        }

        public GameObject Instantiate(string path, Vector2 at)
        {
            var prefab = Resources.Load(path) as GameObject;
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}