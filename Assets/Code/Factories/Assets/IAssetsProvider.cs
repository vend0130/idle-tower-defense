using UnityEngine;

namespace Code.Factories.Assets
{
    public interface IAssetsProvider
    {
        GameObject Instantiate(string path, Vector2 at);
    }
}