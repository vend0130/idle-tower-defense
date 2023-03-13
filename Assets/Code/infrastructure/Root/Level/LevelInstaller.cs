using Code.Controllers.Spawn;
using Code.Controllers.Spells;
using Code.Data;
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
        [Space, SerializeField] private MeteoriteView _meteoriteView;
        [SerializeField] private MeteoriteData _meteoriteData;
        [Space, SerializeField] private ControlOverEnemyData _controlOverEnemyData;
        [Space, SerializeField] private LightningData _lightningData;
        [SerializeField] private LightningView _lightningView;
        [SerializeField, Space] private BloodlustData _bloodlustData;

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
            Container.Bind<LightningView>().FromInstance(_lightningView).AsSingle();
            Container.Bind<BloodlustData>().FromInstance(_bloodlustData).AsSingle();
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
            Container.Bind<MeteoriteData>().FromInstance(_meteoriteData).AsSingle();

            Container.BindInterfacesTo<ControlOverEnemyController>().AsSingle();
            Container.Bind<ControlOverEnemyData>().FromInstance(_controlOverEnemyData).AsSingle();

            Container.BindInterfacesTo<LightningController>().AsSingle();
            Container.Bind<LightningData>().FromInstance(_lightningData).AsSingle();
        }
    }
}