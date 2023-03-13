using UnityEngine;

namespace Code.Factories.AssetsManagement
{
    public interface IAssetsProvider
    {
        GameObject DiInstantiate(GameObject prefab, Vector2 at);
        GameObject Instantiate(string path, Vector2 at);
        GameObject Warmup(string path);
    }
}