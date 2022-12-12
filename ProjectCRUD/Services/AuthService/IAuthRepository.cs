using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCRUD.Services.AuthService
{
    public interface IAuthRepository
    {
        Task<int?> Register(User user, string password); //! Test
        Task<string?> Login(string username, string password);
        Task<bool> UserExists(string username); //! Test
    }
}