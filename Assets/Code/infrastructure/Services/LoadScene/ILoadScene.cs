using Cysharp.Threading.Tasks;

namespace Code.infrastructure.Services.LoadScene
{
    public interface ILoadScene
    {
        UniTask CurtainOnAsync();
        UniTask LoadSceneAsync(string name);
        UniTask CurtainOffAsync();
    }
}