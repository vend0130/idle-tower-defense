using Code.Controllers;
using Code.Factories;
using Code.infrastructure.StateMachine.States;
using Code.Model.Spawn;
using Zenject;

namespace Code.infrastructure.Root.Level
{
    public class LevelInitialize : IInitializable
    {
        private readonly IGameFactory _gameFactory;
        private readonly LoadLevelState _loadLevelState;
        private readonly GameLoopState _gameLoopState;
        private readonly ISpawnController _spawnController;
        private readonly ISpawnModel _spawnModel;

        public LevelInitialize(IGameFactory gameFactory, LoadLevelState loadLevelState,
            GameLoopState gameLoopState, ISpawnController spawnController, ISpawnModel spawnModel)
        {
            _gameFactory = gameFactory;
            _loadLevelState = loadLevelState;
            _gameLoopState = gameLoopState;
            _spawnController = spawnController;
            _spawnModel = spawnModel;
        }

        public void Initialize()
        {
            _spawnModel.Init();
            _gameLoopState.InitSpawnController(_spawnController);
            _loadLevelState.InitGameFactory(_gameFactory);
        }
    }
}