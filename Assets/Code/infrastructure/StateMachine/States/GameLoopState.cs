using Code.Controllers;

namespace Code.infrastructure.StateMachine.States
{
    public class GameLoopState : IDefaultState
    {
        private readonly ISpawnController _spawnController;

        public GameLoopState(ISpawnController spawnController) => 
            _spawnController = spawnController;

        public void Enter() => 
            _spawnController.StartSpawn();

        public void Exit() => 
            _spawnController.EndSpawn();
    }
}