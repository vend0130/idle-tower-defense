using System.Threading.Tasks;

namespace Code.infrastructure.Services.LoadScene
{
    public interface ILoadScene
    {
        Task CurtainOnAsync();
        Task LoadSceneAsync(string name);
        Task CurtainOffAsync();
    }
}