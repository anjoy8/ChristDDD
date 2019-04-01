using System.Threading.Tasks;

namespace Christ3D.Infrastruct.Identity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
