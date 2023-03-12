using System.Threading.Tasks;

namespace Code.Views
{
    public interface ICurtain
    {
        Task FadeOn();
        Task FadeOff();
    }
}