using Code.Controllers.Spawn;
using Code.Controllers.Spells;
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
        private readonly EndGameState _endGameState;
        private readonly ISpawnController _spawnController;
        private readonly ISpawnModel _spawnModel;
        private readonly IMeteoriteController _meteoriteController;
        private readonly IControlOverEnemy _controlOverEnemy;

        public LevelInitialize(IGameFactory gameFactory, LoadLevelState loadLevelState,
            GameLoopState gameLoopState, EndGameState endGameState, ISpawnController spawnController,
            ISpawnModel spawnModel, IMeteoriteController meteoriteController, IControlOverEnemy controlOverEnemy)
        {
            _gameFactory = gameFactory;
            _loadLevelState = loadLevelState;
            _gameLoopState = gameLoopState;
            _endGameState = endGameState;
            _spawnController = spawnController;
            _spawnModel = spawnModel;
            _meteoriteController = meteoriteController;
            _controlOverEnemy = controlOverEnemy;
        }

        public void Initialize()
        {
            _spawnModel.Init();
            _endGameState.InitGameFactory(_gameFactory);
            _gameLoopState.InitSpawnController(_spawnController);
            _loadLevelState.InitLevelData(_gameFactory, _meteoriteController, _controlOverEnemy);
        }
    }
}