using Code.infrastructure.StateMachine;
using Code.infrastructure.StateMachine.States;
using Zenject;

namespace Code.infrastructure.Root
{
    public class BootInitialize : IInitializable
    {
        private const string LevelSceneName = "Level";

        private readonly IStateMachine _stateMachine;

        public BootInitialize(IStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public void Initialize() =>
            _stateMachine.Enter<LoadLevelState, string>(LevelSceneName);
    }
}