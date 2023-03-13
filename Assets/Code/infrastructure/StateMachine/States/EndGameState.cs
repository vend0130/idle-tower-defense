using Code.Factories;
using Code.Game.UI;

namespace Code.infrastructure.StateMachine.States
{
    public class EndGameState : IDefaultState
    {
        private HUD _hud;
        private IGameFactory _gameFactory;

        public void InitGameFactory(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        public void InitHud(HUD hud) =>
            _hud = hud;

        public void Enter()
        {
            _hud.Hide();
            _gameFactory.CreateEndGame();
        }

        public void Exit()
        {
        }
    }
}