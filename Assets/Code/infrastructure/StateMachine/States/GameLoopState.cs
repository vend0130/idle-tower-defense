using Code.Controllers;

namespace Code.infrastructure.StateMachine.States
{
    public class GameLoopState : IDefaultState
    {
        private ISpawnController _spawnController;

        public void InitSpawnController(ISpawnController spawnController) =>
            _spawnController = spawnController;

        public void Enter() =>
            _spawnController.StartSpawn();

        public void Exit() =>
            _spawnController.EndSpawn();
    }
}