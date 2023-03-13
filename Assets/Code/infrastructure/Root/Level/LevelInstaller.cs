using Code.Controllers.Spawn;
using Code.Controllers.Spells;
using Code.Factories;
using Code.Factories.Arrow;
using Code.Factories.AssetsManagement;
using Code.Factories.Enemies;
using Code.Model.Spawn;
using Code.Views.Spells;
using UnityEngine;
using Zenject;

namespace Code.infrastructure.Root.Level
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private MeteoriteView _meteoriteView;

        public override void InstallBindings()
        {
            BindFactories();
            BindSpawnLogic();
            BindInstance();
            BindSpells();

            Container.BindInterfacesTo<LevelInitialize>().AsSingle();
        }

        private void BindInstance()
        {
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
            Container.Bind<MeteoriteView>().FromInstance(_meteoriteView).AsSingle();
        }

        private void BindFactories()
        {
            Container.BindInterfacesTo<AssetsProvider>().AsSingle();
            Container.BindInterfacesTo<GameFactory>().AsSingle();
            Container.BindInterfacesTo<EnemiesFactory>().AsSingle();
            Container.BindInterfacesTo<ArrowFactory>().AsSingle();
        }

        private void BindSpawnLogic()
        {
            Container.BindInterfacesTo<EnemiesSpawnController>().AsSingle();
            Container.BindInterfacesTo<SpawnModel>().AsSingle();
        }

        private void BindSpells()
        {
            Container.BindInterfacesTo<MeteoriteController>().AsSingle();
        }
    }
}