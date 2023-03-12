using System.Threading.Tasks;

namespace Code.infrastructure.Services.LoadScene.Curtain
{
    public interface ICurtain
    {
        Task FadeOn();
        Task FadeOff();
    }
}