using Code.infrastructure.StateMachine;
using Code.infrastructure.StateMachine.States;
using Zenject;

namespace Code.infrastructure.Root.Boot
{
    public class BootInitialize : IInitializable
    {
        private const string LevelSceneName = "Level";

        private readonly IStateMachine _stateMachine;
        private readonly LoadLevelState _loadLevelState;

        public BootInitialize(IStateMachine stateMachine, LoadLevelState loadLevelState)
        {
            _stateMachine = stateMachine;
            _loadLevelState = loadLevelState;
        }

        public void Initialize()
        {
            _loadLevelState.InitGameStateMachine(_stateMachine);
            _stateMachine.Enter<LoadLevelState, string>(LevelSceneName);
        }
    }
}