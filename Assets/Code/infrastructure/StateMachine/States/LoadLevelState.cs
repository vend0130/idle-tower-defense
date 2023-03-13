using System;
using System.Threading;
using Code.Controllers.Spells;
using Code.Factories;
using Code.Game.Hero;
using Code.infrastructure.Services.LoadScene;
using Cysharp.Threading.Tasks;

namespace Code.infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadState<string>, IDisposable
    {
        private readonly ILoadScene _loadScene;
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        private IGameFactory _gameFactory;
        private IStateMachine _stateMachine;
        private HeroHealth _heroHealth;
        private IMeteoriteController _meteoriteController;
        private IControlOverEnemy _controlOverEnemy;
        private ILightningController _lightningController;

        public LoadLevelState(ILoadScene loadScene) =>
            _loadScene = loadScene;

        public void Dispose()
        {
            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
        }

        public void InitGameStateMachine(IStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public void InitLevelData(IGameFactory gameFactory, IMeteoriteController meteoriteController,
            IControlOverEnemy controlOverEnemy, ILightningController lightningController)
        {
            _meteoriteController = meteoriteController;
            _gameFactory = gameFactory;
            _controlOverEnemy = controlOverEnemy;
            _lightningController = lightningController;
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
            _controlOverEnemy.InitSpellView(_gameFactory.CreateSpell(SpellType.ControlOverEnemy));
            _lightningController.InitSpellView(_gameFactory.CreateSpell(SpellType.Lightning));
            await UniTask.Yield(cancellationToken: _cancellationToken.Token);
        }
    }
}