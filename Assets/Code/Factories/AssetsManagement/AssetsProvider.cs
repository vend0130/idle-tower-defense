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

        public GameObject DiInstantiate(GameObject prefab, Vector2 at) =>
            _diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, null);

        public GameObject Instantiate(GameObject prefab, Vector2 at) =>
            Object.Instantiate(prefab, at, Quaternion.identity);
    }
}