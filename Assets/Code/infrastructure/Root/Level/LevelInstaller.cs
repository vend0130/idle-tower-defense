using Code.Controllers;
using Code.Factories;
using Code.Factories.Assets;
using Code.Factories.Enemies;
using Zenject;

namespace Code.infrastructure.Root.Level
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameFactory>().AsSingle();
            Container.BindInterfacesTo<EnemiesFactory>().AsSingle();
            Container.BindInterfacesTo<EnemiesSpawnController>().AsSingle();
            Container.BindInterfacesTo<AssetsProvider>().AsSingle();
            Container.BindInterfacesTo<LevelInitialize>().AsSingle();
        }
    }
}