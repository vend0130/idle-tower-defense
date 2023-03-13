using UnityEngine;

namespace Code.Factories.AssetsManagement
{
    public interface IAssetsProvider
    {
        GameObject DiInstantiate(string path, Vector2 at);
        GameObject Instantiate(string path, Vector2 at);
    }
}