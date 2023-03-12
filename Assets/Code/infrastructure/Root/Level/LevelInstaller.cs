using Code.Factories;
using Zenject;

namespace Code.infrastructure.Root.Level
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameFactory>().AsSingle();
            Container.BindInterfacesTo<LevelInitialize>().AsSingle();
        }
    }
}