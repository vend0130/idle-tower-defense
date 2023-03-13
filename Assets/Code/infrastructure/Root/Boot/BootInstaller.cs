using Code.Data;
using Code.infrastructure.Services.LoadScene;
using Code.infrastructure.StateMachine;
using Code.infrastructure.StateMachine.States;
using Code.Views;
using UnityEngine;
using Zenject;

namespace Code.infrastructure.Root.Boot
{
    public class BootInstaller : MonoInstaller
    {
        [SerializeField] private CurtainView _curtain;
        [SerializeField] private GameData _gameData;

        public override void InstallBindings()
        {
            BindStateMachine();
            BindLoadScene();

            Container.Bind<GameData>().FromInstance(_gameData).AsSingle();

            Container.BindInterfacesTo<BootInitialize>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
            Container.Bind<EndGameState>().AsSingle();
        }

        private void BindLoadScene()
        {
            Container.Bind<ICurtain>().To<CurtainView>().FromInstance(_curtain).AsSingle();
            Container.Bind<ILoadScene>().To<LoadSceneService>().AsSingle();
        }
    }
}