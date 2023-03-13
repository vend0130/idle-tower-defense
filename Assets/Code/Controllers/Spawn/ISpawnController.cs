using UnityEngine;

namespace Code.Controllers.Spawn
{
    public interface ISpawnController
    {
        void StartSpawn();
        void EndSpawn();
        void InitHero(Transform heroTransform);
    }
}