using System;
using Code.Factories.Assets;
using UnityEngine;

namespace Code.Factories.Enemies
{
    public class EnemiesFactory : IEnemiesFactory
    {
        private readonly IAssetsProvider _assetsProvider;

        public EnemiesFactory(IAssetsProvider assetsProvider) =>
            _assetsProvider = assetsProvider;

        public void CreateEnemy(EnemyType enemyType, Vector2 at) =>
            _assetsProvider.Instantiate(GetPath(enemyType), at);

        private string GetPath(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Simply:
                    return AssetPath.SimplyEnemyPath;
                case EnemyType.Boss:
                    return AssetPath.BossEnemyPath;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null);
            }
        }
    }
}