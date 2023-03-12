using Code.Factories.Assets;
using UnityEngine;

namespace Code.Factories.Enemies
{
    public class EnemiesFactory : IEnemiesFactory
    {
        private readonly IAssetsProvider _assetsProvider;

        public EnemiesFactory(IAssetsProvider assetsProvider) =>
            _assetsProvider = assetsProvider;

        public void CreateEnemy(EnemyType enemyType, Vector2 at)
        {
        }
    }
}