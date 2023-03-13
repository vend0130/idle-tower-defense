using Cysharp.Threading.Tasks;

namespace Code.Views
{
    public interface ICurtain
    {
        UniTask FadeOn();
        UniTask FadeOff();
    }
}