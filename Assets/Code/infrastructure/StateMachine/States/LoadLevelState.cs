﻿using Code.Factories;
using Code.infrastructure.Services.LoadScene;
using Cysharp.Threading.Tasks;

namespace Code.infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly ILoadScene _loadScene;

        private IGameFactory _gameFactory;

        public LoadLevelState(ILoadScene loadScene) =>
            _loadScene = loadScene;

        public void InitGameFactory(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        public async void Enter(string sceneName)
        {
            await _loadScene.CurtainOnAsync();
            await _loadScene.LoadSceneAsync(sceneName);
            await UniTask.WaitWhile(() => _gameFactory == null);
            await CreateWorld();
            await _loadScene.CurtainOffAsync();
        }

        public void Exit()
        {
        }

        private async UniTask CreateWorld()
        {
            _gameFactory.CreateHero();
            await UniTask.Yield();
        }
    }
}