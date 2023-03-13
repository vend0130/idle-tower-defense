using Code.Controllers.Spells;
using Code.Factories;
using Code.Game.Hero;
using Code.infrastructure.Services.LoadScene;
using Cysharp.Threading.Tasks;

namespace Code.infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly ILoadScene _loadScene;

        private IGameFactory _gameFactory;
        private IStateMachine _stateMachine;
        private HeroHealth _heroHealth;
        private IMeteoriteController _meteoriteController;

        public LoadLevelState(ILoadScene loadScene) =>
            _loadScene = loadScene;

        public void InitGameStateMachine(IStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public void InitLevelData(IGameFactory gameFactory, IMeteoriteController meteoriteController)
        {
            _meteoriteController = meteoriteController;
            _gameFactory = gameFactory;
        }

        public async void Enter(string sceneName)
        {
            await _loadScene.CurtainOnAsync();
            await _loadScene.LoadSceneAsync(sceneName);
            await UniTask.WaitWhile(() => _gameFactory == null);
            await CreateWorld();
            await _loadScene.CurtainOffAsync();

            _stateMachine.Enter<GameLoopState, HeroHealth>(_heroHealth);
        }

        public void Exit()
        {
        }

        private async UniTask CreateWorld()
        {
            _gameFactory.CreateHud();
            _heroHealth = _gameFactory.CreateHero();
            _meteoriteController.InitSpellView(_gameFactory.CreateSpell(SpellType.Meteorite));
            await UniTask.Yield();
        }
    }
}