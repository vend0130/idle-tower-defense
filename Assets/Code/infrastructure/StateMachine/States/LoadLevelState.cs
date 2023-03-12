using Code.infrastructure.Services.LoadScene;

namespace Code.infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly ILoadScene _loadScene;

        public LoadLevelState(ILoadScene loadScene) =>
            _loadScene = loadScene;

        public async void Enter(string sceneName)
        {
            await _loadScene.CurtainOnAsync();
            await _loadScene.LoadSceneAsync(sceneName);
            await _loadScene.CurtainOffAsync();
        }

        public void Exit()
        {
        }
    }
}