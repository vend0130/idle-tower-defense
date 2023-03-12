using Code.infrastructure.Services.LoadScene.Curtain;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Code.infrastructure.Services.LoadScene
{
    public class LoadSceneService : ILoadScene
    {
        private readonly ICurtain _curtain;

        private bool _firstLoadIsEnded;

        public LoadSceneService(ICurtain curtain) =>
            _curtain = curtain;

        public async UniTask CurtainOnAsync() =>
            await _curtain.FadeOn();

        public async UniTask LoadSceneAsync(string name) =>
            await LoadScene(name);

        public async UniTask CurtainOffAsync() =>
            await _curtain.FadeOff();

        private async UniTask LoadScene(string sceneName)
        {
            var load = SceneManager.LoadSceneAsync(sceneName);

            while (!load.isDone)
                await UniTask.Yield();
        }
    }
}