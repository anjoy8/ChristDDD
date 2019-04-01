using System.Threading.Tasks;

namespace Christ3D.Infrastruct.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
