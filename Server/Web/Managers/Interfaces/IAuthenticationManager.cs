using System;
using System.Threading.Tasks;
using Web.Models.Authentication;

namespace Web.Managers.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<BearerViewModel> AuthenticateAsync(string username, string password);
        string GetUsername(string bearerToken);
    }
}
