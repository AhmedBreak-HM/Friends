using System.Threading.Tasks;
using ZwajApp.API.Models;

namespace ZwajApp.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Register (User user, string password);
        Task<User> logIn (string userName, string password);
        Task<bool> UserExists(string userName);
    }
}