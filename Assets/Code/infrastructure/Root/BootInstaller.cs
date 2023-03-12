using Code.infrastructure.StateMachine;
using Code.infrastructure.StateMachine.States;
using Zenject;

namespace Code.infrastructure.Root
{
    public class BootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStateMachine();

            Container.BindInterfacesTo<BootInitialize>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
        }
    }
}