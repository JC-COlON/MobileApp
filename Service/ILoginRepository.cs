

using DigesettAPP.Models;

namespace DigesettAPP.Service
{
   public interface ILoginRepository
    {
        Task<User> Login(string username, string password);
        
    }
}
