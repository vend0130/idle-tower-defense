using Code.Controllers.Spawn;
using Code.Factories;
using Code.Factories.Assets;
using Code.Factories.Enemies;
using Code.Model.Spawn;
using UnityEngine;
using Zenject;

namespace Code.infrastructure.Root.Level
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;

        public override void InstallBindings()
        {
            BindFactories();
            BindSpawnLogic();
            BindCamera();

            Container.BindInterfacesTo<LevelInitialize>().AsSingle();
        }

        private void BindCamera() =>
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();

        private void BindFactories()
        {
            Container.BindInterfacesTo<AssetsProvider>().AsSingle();
            Container.BindInterfacesTo<GameFactory>().AsSingle();
            Container.BindInterfacesTo<EnemiesFactory>().AsSingle();
        }

        private void BindSpawnLogic()
        {
            Container.BindInterfacesTo<EnemiesSpawnController>().AsSingle();
            Container.BindInterfacesTo<SpawnModel>().AsSingle();
        }
    }
}