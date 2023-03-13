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
        private readonly GameLoopState _gameLoopState;

        public BootInitialize(IStateMachine stateMachine, LoadLevelState loadLevelState, GameLoopState gameLoopState)
        {
            _stateMachine = stateMachine;
            _loadLevelState = loadLevelState;
            _gameLoopState = gameLoopState;
        }

        public void Initialize()
        {
            _loadLevelState.InitGameStateMachine(_stateMachine);
            _gameLoopState.InitGameStateMachine(_stateMachine);
            _stateMachine.Enter<LoadLevelState, string>(LevelSceneName);
        }
    }
}