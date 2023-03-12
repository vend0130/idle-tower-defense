using System.Threading.Tasks;
using Code.infrastructure.Services.LoadScene.Curtain;
using UnityEngine.SceneManagement;

namespace Code.infrastructure.Services.LoadScene
{
    public class LoadSceneService : ILoadScene
    {
        private readonly ICurtain _curtain;

        private bool _firstLoadIsEnded;

        public LoadSceneService(ICurtain curtain) =>
            _curtain = curtain;

        public async Task CurtainOnAsync() =>
            await _curtain.FadeOn();

        public async Task LoadSceneAsync(string name) =>
            await LoadScene(name);

        public async Task CurtainOffAsync() =>
            await _curtain.FadeOff();

        private async Task LoadScene(string sceneName)
        {
            var load = SceneManager.LoadSceneAsync(sceneName);

            while (!load.isDone)
                await Task.Yield();
        }
    }
}