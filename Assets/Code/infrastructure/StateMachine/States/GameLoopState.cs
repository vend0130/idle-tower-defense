using Code.Controllers.Spawn;
using Code.Game.Hero;

namespace Code.infrastructure.StateMachine.States
{
    public class GameLoopState : IPayloadState<HeroHealth>
    {
        private ISpawnController _spawnController;
        private HeroHealth _heroHealth;
        private IStateMachine _stateMachine;

        public void InitGameStateMachine(IStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public void InitSpawnController(ISpawnController spawnController) =>
            _spawnController = spawnController;

        public void Enter(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _spawnController.StartSpawn();

            _heroHealth.DieHandler += HeroDie;
        }

        public void Exit() =>
            _heroHealth.DieHandler -= HeroDie;

        private void HeroDie()
        {
            _spawnController.EndSpawn();
            _stateMachine.Enter<EndGameState>();
        }
    }
}