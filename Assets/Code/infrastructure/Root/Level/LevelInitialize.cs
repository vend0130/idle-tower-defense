using Code.Factories;
using Code.infrastructure.StateMachine.States;
using Zenject;

namespace Code.infrastructure.Root.Level
{
    public class LevelInitialize : IInitializable
    {
        private readonly IGameFactory _gameFactory;
        private readonly LoadLevelState _loadLevelState;

        public LevelInitialize(IGameFactory gameFactory, LoadLevelState loadLevelState)
        {
            _gameFactory = gameFactory;
            _loadLevelState = loadLevelState;
        }
        
        public void Initialize() => 
            _loadLevelState.InitGameFactory(_gameFactory);
    }
}