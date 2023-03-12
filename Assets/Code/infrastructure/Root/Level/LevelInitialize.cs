using Code.Controllers;
using Code.Factories;
using Code.infrastructure.StateMachine.States;
using Zenject;

namespace Code.infrastructure.Root.Level
{
    public class LevelInitialize : IInitializable
    {
        private readonly IGameFactory _gameFactory;
        private readonly LoadLevelState _loadLevelState;
        private readonly GameLoopState _gameLoopState;
        private readonly ISpawnController _spawnController;

        public LevelInitialize(IGameFactory gameFactory, LoadLevelState loadLevelState,
            GameLoopState gameLoopState, ISpawnController spawnController)
        {
            _gameFactory = gameFactory;
            _loadLevelState = loadLevelState;
            _gameLoopState = gameLoopState;
            _spawnController = spawnController;
        }

        public void Initialize()
        {
            _gameLoopState.InitSpawnController(_spawnController);
            _loadLevelState.InitGameFactory(_gameFactory);
        }
    }
}